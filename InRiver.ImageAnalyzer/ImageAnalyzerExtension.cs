using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Image;
using inRiver.Remoting.Log;
using inRiver.Remoting.Objects;
using InRiver.ImageAnalyzer.Models;
using Newtonsoft.Json;

namespace InRiver.ImageAnalyzer
{
    public class ImageAnalyzerExtension : IEntityListener
    {
        private const string KeyAzureApiKey = "AZURE_APIKEY";
        private const string KeyAzureEndpointUrl = "AZURE_ENDPOINT_URL";
        private const string KeyResourceEntityTypeId = "RESOURCE_ENTITY_TYPE_ID";
        private const string KeyResourceCaptionFieldId = "RESOURCE_CAPTION_FIELD_ID";
        private const string KeyResourceFileIdFieldId = "RESOURCE_FILEID_FIELD_ID";
        private const string KeyResourceTagsFieldId = "RESOURCE_TAGS_FIELD_ID";
        private const string KeyAddUnknownCvlValues = "ADD_UNKNOWN_CVL_VALUES";
        private const string KeyTagsMinimumConfidence = "TAGS_MINIMUM_CONFICENDE";

        private static readonly HttpClient HttpClient = new HttpClient();

        public inRiverContext Context { get; set; }

        public Dictionary<string, string> DefaultSettings => new Dictionary<string, string>
        {
            { KeyAzureApiKey, "" },
            { KeyAzureEndpointUrl, "" },
            { KeyResourceEntityTypeId, "Resource" },
            { KeyResourceFileIdFieldId, "ResourceFileId" },
            { KeyResourceCaptionFieldId, "ResourceCaption" },
            { KeyResourceTagsFieldId, "ResourceTags" },
            { KeyAddUnknownCvlValues, "true" },
            { KeyTagsMinimumConfidence, "0.5" }
        };

        private string AzureApiKey => GetSettingOrDefault(KeyAzureApiKey);
        private string AzureEndpointUrl => GetSettingOrDefault(KeyAzureEndpointUrl);
        private string ResourceEntityTypeId => GetSettingOrDefault(KeyResourceEntityTypeId);
        private string ResourceCaptionFieldId => GetSettingOrDefault(KeyResourceCaptionFieldId);
        private string ResourceFileIdFieldId => GetSettingOrDefault(KeyResourceFileIdFieldId);
        private string ResourceTagsFieldId => GetSettingOrDefault(KeyResourceTagsFieldId);
        private bool AddUnknownCvlValues => GetBooleanSetting(KeyAddUnknownCvlValues);
        private decimal MinimumConfidence => GetDecimalSetting(KeyTagsMinimumConfidence);

        public string Test()
        {
            bool isValid = ValidateSettings();

            return isValid
                ? "SUCCESS: The settings were valid."
                : "ERROR: Some settings are invalid. See log entries.";
        }

        public void EntityCreated(int entityId)
        {
            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            Entity resourceEntity = Context.ExtensionManager.DataService.GetEntity(entityId, LoadLevel.DataOnly);
            if (resourceEntity == null ||
                resourceEntity.EntityType.Id != ResourceEntityTypeId)
            {
                // If entity was not found, or if it is not as resource entity.
                return;
            }

            int fileId = (int)resourceEntity.GetField(ResourceFileIdFieldId).Data;
            byte[] fileData = Context.ExtensionManager.UtilityService.GetFile(fileId, ImageConfiguration.Original);
            if (fileData == null || fileData.Length == 0)
            {
                // If the file is emtpty or not found.
                return;
            }

            // Analyze and update fields.
            UpdateMetadata(resourceEntity, fileData);
        }

        public void EntityUpdated(int entityId, string[] fields)
        {
            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            if (!fields.Contains(ResourceFileIdFieldId))
            {
                // If the resource file id is not among the updated fields.
                return;
            }

            Entity resourceEntity = Context.ExtensionManager.DataService.GetEntity(entityId, LoadLevel.DataOnly);
            if (resourceEntity == null ||
                resourceEntity.EntityType.Id != ResourceEntityTypeId)
            {
                // If entity was not found, or if it is not as resource entity.
                return;
            }

            int fileId = (int)resourceEntity.GetField(ResourceFileIdFieldId).Data;
            byte[] fileData = Context.ExtensionManager.UtilityService.GetFile(fileId, ImageConfiguration.Original);
            if (fileData == null || fileData.Length == 0)
            {
                // If the file is emtpty or not found.
                return;
            }

            // Analyze and update fields.
            UpdateMetadata(resourceEntity, fileData);
        }

        #region Unimplemented methods
        public void EntityDeleted(Entity deletedEntity)
        {
        }

        public void EntityLocked(int entityId)
        {
        }

        public void EntityUnlocked(int entityId)
        {
        }

        public void EntityFieldSetUpdated(int entityId, string fieldSetId)
        {
        }

        public void EntityCommentAdded(int entityId, int commentId)
        {
        }

        public void EntitySpecificationFieldAdded(int entityId, string fieldName)
        {
        }

        public void EntitySpecificationFieldUpdated(int entityId, string fieldName)
        {
        }
        #endregion

        private void UpdateMetadata(Entity resourceEntity, byte[] fileContent)
        {
            AnalyzeImageResponse analysisResponse =
                Task.Run(async () => await AnalyzeImage(fileContent)).Result;
            if (analysisResponse == null)
            {
                Context.Log(LogLevel.Error, "No valid API response received.");
                return;
            }

            var modifiedFields = new List<Field>(2);

            Field tagsField = resourceEntity.GetField(ResourceTagsFieldId);
            if (tagsField != null)
            {
                // If the tag field exists, set the tags that meets the minimum confidence level.
                string[] tagNames = analysisResponse.Tags
                    .Where(tag => tag.Confidence >= MinimumConfidence)
                    .Select(tag => tag.Name)
                    .ToArray();

                IEnumerable<string> tagCvlValues = GetCvlValues(tagsField.FieldType, tagNames);
                tagsField.Data = string.Join(";", tagCvlValues);
                modifiedFields.Add(tagsField);
            }

            Field captionField = resourceEntity.GetField(ResourceCaptionFieldId);
            if (captionField != null)
            {
                // If the tag field exists, set the best caption that meets the minimum confidence level.
                string captionText = analysisResponse.Description?.Captions?
                    .Where(cap => cap.Confidence >= MinimumConfidence)
                    .OrderByDescending(cap => cap.Confidence)
                    .Select(cap => cap.Text)
                    .FirstOrDefault();

                captionField.Data = captionText;
                modifiedFields.Add(captionField);
            }

            if (modifiedFields.Count == 0)
            {
                return;
            }

            Context.ExtensionManager.DataService.UpdateFieldsForEntity(modifiedFields);
        }

        private async Task<AnalyzeImageResponse> AnalyzeImage(byte[] fileContent)
        {
            if (fileContent == null ||
                fileContent.Length == 0)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            string requestParameters = "visualFeatures=Tags,Categories,Description,Color&language=en";

            // Assemble the URI for the REST API Call.
            string uri = $"{AzureEndpointUrl}/analyze?{requestParameters}";

            using (ByteArrayContent content = new ByteArrayContent(fileContent))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Headers.Add("Ocp-Apim-Subscription-Key", AzureApiKey);

                // Execute the REST API call.
                var response = await HttpClient.PostAsync(uri, content);

                // If the API returned any error code, throw an exception.
                response.EnsureSuccessStatusCode();

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                var analysisResponse = JsonConvert.DeserializeObject<AnalyzeImageResponse>(contentString);

                return analysisResponse;
            }
        }

        private IEnumerable<string> GetCvlValues(FieldType fieldType, string[] newValues)
        {
            if (fieldType == null)
            {
                throw new ArgumentNullException(nameof(fieldType));
            }

            if (newValues == null ||
                newValues.Length == 0)
            {
                yield break;
            }

            string cvlId = fieldType.CVLId;
            List<CVLValue> knownValues = Context.ExtensionManager.ModelService.GetCVLValuesForCVL(cvlId);

            foreach (string newValue in newValues)
            {
                string cvlKey = newValue.RemoveSpecialCharacters();

                // If the key is already known, return it.
                if (knownValues.Any(v => v.Key == cvlKey))
                {
                    yield return cvlKey;
                    continue;
                }

                // If the key is not known and adding new values is disabled, skip this.
                if (!AddUnknownCvlValues)
                {
                    continue;
                }

                // Add the new CVLValue and return the key.
                CVLValue cvlValue = new CVLValue {CVLId = cvlId, Key = cvlKey, Value = newValue};
                cvlValue = Context.ExtensionManager.ModelService.AddCVLValue(cvlValue);
                Context.Log(LogLevel.Information, $"A new CVL value was added (Key: {cvlKey}; Value: {newValue}). ");

                yield return cvlValue.Key;
            }
        }

        #region Settings
        private bool ValidateSettings()
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(KeyAzureApiKey))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyAzureApiKey}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyAzureEndpointUrl)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyAzureEndpointUrl}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyResourceEntityTypeId)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyResourceEntityTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyResourceCaptionFieldId)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyResourceCaptionFieldId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyResourceFileIdFieldId)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyResourceFileIdFieldId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyResourceTagsFieldId)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyResourceTagsFieldId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyAddUnknownCvlValues)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyAddUnknownCvlValues}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(KeyTagsMinimumConfidence)) {
                Context.Log(LogLevel.Error, $"The setting '{KeyTagsMinimumConfidence}' is empty.");
                valid = false;
            }

            return valid;
        }

        private string GetSettingOrDefault(string key)
        {
            return Context.Settings.TryGetValue(key, out string value)
                ? value
                : null;
        }

        private bool GetBooleanSetting(string key)
        {
            string value = GetSettingOrDefault(key);

            return bool.TryParse(value, out bool result) && result;
        }

        private decimal GetDecimalSetting(string key)
        {
            string value = GetSettingOrDefault(key);

            return !string.IsNullOrEmpty(value) &&
                   decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result)
                ? result
                : default(decimal);
        }

        private int GetIntValue(string key)
        {
            string value = GetSettingOrDefault(key);

            return !string.IsNullOrEmpty(value) &&
                   int.TryParse(value, out int result)
                ? result
                : default(int);
        }
        #endregion
    }
}
