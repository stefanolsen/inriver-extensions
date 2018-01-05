using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Image;
using inRiver.Remoting.Objects;
using Newtonsoft.Json;

namespace InRiver.ImageAnalyzer
{
    public class ImageAnalyzerExtension : IEntityListener
    {
        private const string KeyAzureApiKey = "AZURE_APIKEY";
        private const string KeyAzureEndpointUrl = "AZURE_ENDPOINT_URL";
        private const string KeyResourceEntityTypeId = "RESOURCE_ENTITY_TYPE_ID";
        private const string KeyResourceFileIdFieldId = "RESOURCE_FILEID_FIELD_ID";
        private const string KeyResourceTypeFieldId = "RESOURCE_TYPE_FIELD_ID";
        private const string KeyIncludedResourceTypes = "INCLUDED_RESOURCE_TYPES";

        public inRiverContext Context { get; set; }

        public Dictionary<string, string> DefaultSettings => new Dictionary<string, string>
        {
            { KeyAzureApiKey, "" },
            { KeyAzureEndpointUrl, "" },
            { KeyResourceEntityTypeId, "Resource" },
            { KeyResourceFileIdFieldId, "ResourceType" },
            { KeyResourceTypeFieldId, "ResourceType" },
            { KeyIncludedResourceTypes, "default,carousel" }
        };

        private string AzureApiKey => GetSettingOrDefault(KeyAzureApiKey);
        private string AzureEndpointUrl => GetSettingOrDefault(KeyAzureEndpointUrl);
        private string ResourceEntityTypeId => GetSettingOrDefault(KeyResourceEntityTypeId);
        private string ResourceFileIdFieldId => GetSettingOrDefault(KeyResourceTypeFieldId);
        private string ResourceTypeFieldId => GetSettingOrDefault(KeyResourceTypeFieldId);
        private string IncludedResourceTypes => GetSettingOrDefault(KeyIncludedResourceTypes);

        public string Test()
        {
            Task.Run(async () => await CreateRequest()).Wait();

            throw new NotImplementedException();
        }

        private async Task CreateRequest()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AzureApiKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "visualFeatures=Tags,Categories,Description,Color&language=en";

            // Assemble the URI for the REST API Call.
            string uri = $"{AzureEndpointUrl}/analyze?{requestParameters}";

            byte[] data = File.ReadAllBytes(
                @"C:\Repos\Blog\InRiverExtensions\InRiver.ImageAnalyzer.Tests\testimage.jpg");

            using (ByteArrayContent content = new ByteArrayContent(data))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                var response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                var returnData = JsonConvert.DeserializeObject<AnalyzeImageResponse>(contentString);
            }
        }

        public void EntityCreated(int entityId)
        {
            Entity resourceEntity = Context.ExtensionManager.DataService.GetEntity(entityId, LoadLevel.DataAndLinks);
            if (resourceEntity == null ||
                resourceEntity.EntityType.Id != ResourceEntityTypeId)
            {
                // If entity was not found, or if it is not an item entity.
                return;
            }

            int fileId = (int)resourceEntity.GetField(ResourceFileIdFieldId).Data;
            byte[] fileData = Context.ExtensionManager.UtilityService.GetFile(fileId, ImageConfiguration.Original);
            if (fileData == null || fileData.Length == 0)
            {
                return;
            }

            // Do the thing.
        }

        public void EntityUpdated(int entityId, string[] fields)
        {
            if (!fields.Contains(ResourceFileIdFieldId) ||
                !fields.Contains(ResourceTypeFieldId))
            {
                // If neither resource file id nor resource type is among the updated fields.
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
                return;
            }

            // Do the thing.
        }

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

        private string GetSettingOrDefault(string key)
        {
            return Context.Settings.TryGetValue(key, out string value)
                ? value
                : null;
        }
    }
}
