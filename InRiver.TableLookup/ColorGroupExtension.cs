using System;
using System.Collections.Generic;
using System.Linq;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Objects;
using InRiver.TableLookupExtension;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using LogLevel = inRiver.Remoting.Log.LogLevel;

namespace InRiver.TableLookup
{
    public class ColorGroupExtension : IEntityListener, ILinkListener
    {
        private const string KeyItemEntityTypeId = "ITEM_ENTITY_TYPE_ID";
        private const string KeyProductEntityTypeId = "PRODUCT_ENTITY_TYPE_ID";
        private const string KeyProductItemLinkTypeId = "PRODUCTITEM_LINK_TYPE_ID";
        private const string KeyBrandFieldTypeId = "BRAND_FIELD_TYPE_ID";
        private const string KeyColorIdFieldTypeId = "COLORID_FIELD_TYPE_ID";
        private const string KeyColorGroupFieldTypeId = "COLORGROUP_FIELD_TYPE_ID";
        private const string KeyTableConnectionString = "TABLE_CONNECTION_STRING";
        private const string KeyTableName = "TABLE_NAME";

        public inRiverContext Context { get; set; }

        public Dictionary<string, string> DefaultSettings => new Dictionary<string, string>
        {
            { KeyItemEntityTypeId, "Item" },
            { KeyProductEntityTypeId, "Product" },
            { KeyProductItemLinkTypeId, "ProductItem" },
            { KeyBrandFieldTypeId, "ProductBrand" },
            { KeyColorIdFieldTypeId, "ItemColorId" },
            { KeyColorGroupFieldTypeId, "ItemColorGroup" },
            { KeyTableConnectionString, "" },
            { KeyTableName, "" }
        };

        private string ItemEntityTypeId => GetSettingOrDefault(KeyItemEntityTypeId);
        private string ProductEntityTypeId => GetSettingOrDefault(KeyProductEntityTypeId);
        private string ProductItemLinkTypeId => GetSettingOrDefault(KeyProductItemLinkTypeId);

        private string BrandFieldTypeId => GetSettingOrDefault(KeyBrandFieldTypeId);
        private string ColorIdFieldTypeId => GetSettingOrDefault(KeyColorIdFieldTypeId);
        private string ColorGroupFieldTypeId => GetSettingOrDefault(KeyColorGroupFieldTypeId);

        private string TableConnectionString => GetSettingOrDefault(KeyTableConnectionString);
        private string TableName => GetSettingOrDefault(KeyTableName);

        public string Test()
        {
            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return "ERROR: Some settings are invalid. See log entries.";
            }

            CloudStorageAccount account = GetStorageAccount(TableConnectionString);
            if (account == null)
            {
                return "ERROR: Connection string is invalid.";
            }

            // Connect to Azure Table Storage and verify that the specified table exists.
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(TableName);
            bool tableExists = table.Exists();

            return tableExists
                ? "SUCCESS: The table was found."
                : "ERROR: The table was not found.";
        }

        public void EntityCreated(int entityId)
        {
            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            Entity itemEntity = Context.ExtensionManager.DataService.GetEntity(entityId, LoadLevel.DataAndLinks);
            if (itemEntity == null ||
                itemEntity.EntityType.Id != ItemEntityTypeId)
            {
                // If entity was not found, or if it is not an item entity.
                return;
            }

            Entity productEntity = itemEntity.InboundLinks
                .FirstOrDefault(l => l.LinkType.Id == ProductItemLinkTypeId && !l.Inactive)?
                .Source;
            if (productEntity == null)
            {
                Context.Log(LogLevel.Information, "The item has no active links to a product.");
                return;
            }

            UpdateColorGroup(productEntity, itemEntity);
        }

        public void EntityUpdated(int entityId, string[] fields)
        {
            if (!fields.Contains(BrandFieldTypeId) &&
                !fields.Contains(ColorIdFieldTypeId))
            {
                // If neither brand field nor color id field is among the updated fields.
                return;
            }

            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            Entity entity = Context.ExtensionManager.DataService.GetEntity(entityId, LoadLevel.DataAndLinks);
            if (entity == null)
            {
                // If entity was not found.
                return;
            }

            EntityType entityType = entity.EntityType;
            if (entityType.Id == ItemEntityTypeId)
            {
                // If the entity is an item entity.
                Entity productEntity = entity.InboundLinks
                    .FirstOrDefault(l => l.LinkType.Id == ProductItemLinkTypeId && !l.Inactive)?
                    .Source;
                if (productEntity == null)
                {
                    // If the product entity is null.
                    Context.Log(LogLevel.Information, "The item has no active links to a product.");

                    return;
                }

                UpdateColorGroup(productEntity, entity);
            }
            else if (entityType.Id == ProductEntityTypeId)
            {
                // If the entity is a product entity.
                Entity[] itemEntities = entity.OutboundLinks
                    .Where(l => l.LinkType.Id == ProductItemLinkTypeId && !l.Inactive)
                    .Select(l => l.Target)
                    .ToArray();
                if (itemEntities.Length == 0)
                {
                    // If the item entity array is empty.
                    Context.Log(LogLevel.Information, "The product has no active links to any items.");

                    return;
                }

                // For each of the item entities, update the color group.
                foreach (Entity itemEntity in itemEntities)
                {
                    UpdateColorGroup(entity, itemEntity);
                }
            }
        }

        public void LinkCreated(int linkId, int sourceId, int targetId, string linkTypeId, int? linkEntityId)
        {
            if (linkTypeId != ProductItemLinkTypeId)
            {
                // If the link is not between a product and an item.
                return;
            }

            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            Entity productEntity = Context.ExtensionManager.DataService.GetEntity(sourceId, LoadLevel.DataOnly);
            Entity itemEntity = Context.ExtensionManager.DataService.GetEntity(targetId, LoadLevel.DataOnly);
            if (productEntity == null || itemEntity == null)
            {
                // If either the product or the item entity is null.
                return;
            }

            UpdateColorGroup(productEntity, itemEntity);
        }

        public void LinkUpdated(int linkId, int sourceId, int targetId, string linkTypeId, int? linkEntityId)
        {
            if (linkTypeId != ProductItemLinkTypeId)
            {
                // If the link is not between a product and an item.
                return;
            }

            bool isValid = ValidateSettings();
            if (!isValid)
            {
                // If the settings are invalid.
                return;
            }

            Entity productEntity = Context.ExtensionManager.DataService.GetEntity(sourceId, LoadLevel.DataOnly);
            Entity itemEntity = Context.ExtensionManager.DataService.GetEntity(targetId, LoadLevel.DataOnly);
            if (productEntity == null || itemEntity == null)
            {
                // If either the product or the item entity is null.
                return;
            }

            UpdateColorGroup(productEntity, itemEntity);
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

        public void LinkDeleted(int linkId, int sourceId, int targetId, string linkTypeId, int? linkEntityId)
        {
        }

        public void LinkActivated(int linkId, int sourceId, int targetId, string linkTypeId, int? linkEntityId)
        {
        }

        public void LinkInactivated(int linkId, int sourceId, int targetId, string linkTypeId, int? linkEntityId)
        {
        }
        #endregion

        private CloudStorageAccount GetStorageAccount(string connectionString)
        {
            try
            {
                // Try to parse the connection string.
                return CloudStorageAccount.Parse(connectionString);
            }
            catch (Exception ex)
            {
                Context.Log(LogLevel.Error, $"Setting {KeyTableConnectionString} is not a valid connection string.", ex);

                return null;
            }
        }

        private ColorGroup LookupColorGroup(string brandName, string colorId)
        {
            if (string.IsNullOrWhiteSpace(brandName))
            {
                throw new ArgumentNullException(nameof(brandName));
            }

            if (string.IsNullOrWhiteSpace(colorId))
            {
                throw new ArgumentNullException(nameof(colorId));
            }

            CloudStorageAccount account = GetStorageAccount(TableConnectionString);
            if (account == null)
            {
                return null;
            }

            // Connect to Azure Table Storage.
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(TableName);

            // Find a color group for the specific combination of brand name and color id.
            TableOperation retrieveOperation = TableOperation.Retrieve<ColorGroup>(brandName, colorId);
            TableResult tableResult = table.Execute(retrieveOperation);

            ColorGroup colorGroup = tableResult?.Result as ColorGroup;

            return colorGroup;
        }

        private void UpdateColorGroup(Entity productEntity, Entity itemEntity)
        {
            if (productEntity.LoadLevel == LoadLevel.Shallow)
            {
                productEntity = Context.ExtensionManager.DataService.GetEntity(productEntity.Id, LoadLevel.DataOnly);
            }

            if (itemEntity.LoadLevel == LoadLevel.Shallow)
            {
                itemEntity = Context.ExtensionManager.DataService.GetEntity(itemEntity.Id, LoadLevel.DataOnly);
            }

            string brandName = productEntity.GetField(BrandFieldTypeId)?.Data as string;
            string colorId = itemEntity.GetField(ColorIdFieldTypeId)?.Data as string;

            Field colorGroupField = itemEntity.GetField(ColorGroupFieldTypeId);
            if (colorGroupField == null)
            {
                Context.Log(LogLevel.Error,
                    $"Entity type {ItemEntityTypeId} has no field named {ColorGroupFieldTypeId}.");
                return;
            }

            // Look up the color group by brand name and color id.
            ColorGroup colorGroup = LookupColorGroup(brandName, colorId);
            if (colorGroup == null)
            {
                // If no color group was found.
                Context.Log(LogLevel.Warning, "No color group was found.");
            }

            object colorGroupFieldData = colorGroupField.Data;

            // Set the color group CVL key (or null if it does not exist) and save the entity.
            colorGroupField.Data = colorGroup?.CvlKey;

            if (!colorGroupField.ValueHasBeenModified(colorGroupFieldData))
            {
                // If the value has not changed.
                Context.Log(LogLevel.Debug, $"The color group (on {itemEntity}) was not modified.");

                return;
            }

            var modifiedFields = new List<Field>(1) {colorGroupField};

            Context.ExtensionManager.DataService.UpdateFieldsForEntity(modifiedFields);

            Context.Log(LogLevel.Information, $"The color group (on {itemEntity}) was succesfully updated.");
        }

        private string GetSettingOrDefault(string key)
        {
            return Context.Settings.TryGetValue(key, out string value)
                ? value
                : null;
        }

        private bool ValidateSettings()
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(ItemEntityTypeId))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyItemEntityTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ProductItemLinkTypeId))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyProductItemLinkTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(BrandFieldTypeId))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyBrandFieldTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ColorIdFieldTypeId))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyColorIdFieldTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ColorGroupFieldTypeId))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyColorGroupFieldTypeId}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(TableConnectionString))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyTableConnectionString}' is empty.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(TableName))
            {
                Context.Log(LogLevel.Error, $"The setting '{KeyTableName}' is empty.");
                valid = false;
            }

            return valid;
        }
    }
}
