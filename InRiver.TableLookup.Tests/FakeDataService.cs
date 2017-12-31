using System;
using System.Collections.Generic;
using System.Globalization;
using inRiver.Remoting;
using inRiver.Remoting.Objects;
using inRiver.Remoting.Query;

namespace InRiver.TableLookup.Tests
{
    internal class FakeDataService : IDataService
    {
        private readonly Dictionary<int, Entity> _entities;
        private readonly Dictionary<int, List<Link>> _links;

        public FakeDataService()
        {
            _entities = new Dictionary<int, Entity>
            {
                {
                    1, new Entity
                    {
                        EntityType = new EntityType("Item"),
                        Fields = new List<Field>
                        {
                            new Field
                            {
                                FieldType = new FieldType("ItemColorGroup", "Item", DataType.CVL, "Appearance")
                            },
                            new Field
                            {
                                FieldType = new FieldType("ItemColorId", "Item", DataType.String, "Appearance"),
                                Data = "057"
                            }
                        },
                        LoadLevel = LoadLevel.DataAndLinks
                    }
                },
                {
                    2, new Entity
                    {
                        EntityType = new EntityType("Product"),
                        Fields = new List<Field>
                        {
                            new Field
                            {
                                FieldType = new FieldType("ProductBrand", "Product", DataType.CVL, "General"),
                                Data = "mckinley"
                            }
                        }
                    }
                }
            };

            _links = new Dictionary<int, List<Link>>
            {
                {
                    3, new List<Link>
                    {
                        new Link
                        {
                            Id = 3,
                            LinkType = new LinkType {Id = "ProductItem"},
                            Source = _entities[2],
                            Target = _entities[1]
                        }
                    }
                }
            };

            _entities[1].Links = _links[3];
            _entities[2].Links = _links[3];
        }

        public Entity AddEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Entity GetEntity(int id, LoadLevel level)
        {
            return _entities.TryGetValue(id, out Entity entity) ? entity : null;
        }

        public bool DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllEntities()
        {
            throw new NotImplementedException();
        }

        public Entity UpdateEntity(Entity entity)
        {
            return entity;
        }

        public Entity UpdateFieldsForEntity(List<Field> fields)
        {
            throw new NotImplementedException();
        }

        public Field GetField(int entityId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public object GetFieldValue(int entityId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetFieldsForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetFields(int entityId, List<string> fieldTypeIds)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetFieldHistory(int entityId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<FieldRevision> GetFieldRevisions(int entityId, string fieldTypeId, int maxNumberOfRevisions)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetAllFieldsByFieldType(string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public Entity CreateNewVersion(int entityId)
        {
            throw new NotImplementedException();
        }

        public Entity GetEntityVersion(int entityId, int version)
        {
            throw new NotImplementedException();
        }

        public Entity GetEntityByUniqueValue(string fieldTypeId, string value, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetEntities(List<int> idList, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public Entity LockEntity(int id, string username)
        {
            throw new NotImplementedException();
        }

        public Entity UnLockEntity(int id)
        {
            throw new NotImplementedException();
        }

        public bool UnLockEntities(List<int> entityIds)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetAllLockedEntities()
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetAllLockedEntitiesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetAllEntitiesForEntityType(string entityTypeId, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetEntitiesForEntityType(int maxCount, string entityTypeId, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetResourcesForLightBoard(int entityId, bool includeSubEntitesResources)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetEntityResourcesForLightBoard(List<int> entityIds, bool includeSubEntitesResources)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetResourcesForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetResourcesForEntities(List<int> entityIds)
        {
            throw new NotImplementedException();
        }

        public List<EntityHistory> GetEntityHistory(int entityId)
        {
            throw new NotImplementedException();
        }

        public Entity SetEntityFieldSet(int entityId, string fieldSetId)
        {
            throw new NotImplementedException();
        }

        public Entity RemoveEntityFieldSet(int entityId)
        {
            throw new NotImplementedException();
        }

        public Entity SetMainPicture(int entityId, int resourceId)
        {
            throw new NotImplementedException();
        }

        public int GetEntityCountForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetAllEntityIdsForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllFieldValuesForField(string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllFieldValuesForFieldCaseSensitive(string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public void ReCalculateDisplayValuesForAllEntities()
        {
            throw new NotImplementedException();
        }

        public void ReCalculateMainPictureForAllEntities()
        {
            throw new NotImplementedException();
        }

        public Link AddLink(Link link)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLink(int linkId)
        {
            throw new NotImplementedException();
        }

        public Link UpdateLinkSortOrder(int linkId, int index)
        {
            throw new NotImplementedException();
        }

        public Link GetLink(int linkId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetLinksForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetInboundLinksForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetOutboundLinksForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool LinkAlreadyExists(int sourceEntityId, int targetEntityId, int? linkEntityId, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public Link FindLink(int sourceEntityId, int targetEntityId, int? linkEntityId, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinks(List<int> linkIds)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllLinks()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllLinksForLinkType(string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public bool AddLinks(List<Link> links)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLinks(List<Link> links)
        {
            throw new NotImplementedException();
        }

        public bool Inactivate(int id)
        {
            throw new NotImplementedException();
        }

        public bool Activate(int id)
        {
            throw new NotImplementedException();
        }

        public Link AddLinkLast(Link link)
        {
            throw new NotImplementedException();
        }

        public Link AddLinkFirst(Link link)
        {
            throw new NotImplementedException();
        }

        public Link AddLinkAt(Link link, int index)
        {
            throw new NotImplementedException();
        }

        public bool AddLinksForEntityAndLinkTypeAt(List<Link> links, int index)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetOutboundLinksForEntityAndLinkType(int entityId, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Entity, List<Link>> GetOutboundLinksForEntitiesAndLinkType(List<int> entityIds, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Entity, List<Link>> GetInboundLinksForEntitiesAndLinkType(List<int> entityIds, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Entity, List<Link>> GetLinksForLinkEntities(List<int> entityIds)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetInboundLinksForEntityAndLinkType(int entityId, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetLinksForLinkEntity(int linkEntityId)
        {
            throw new NotImplementedException();
        }

        public int GetLinkCountForLinkType(string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessDetail> GetEntityCompletenessDetails(int entityId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> GetAssortmentLinkStructureCount(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetOutboundLinksForEntityAndLinkTypeAndLinkEntity(int entityId, string linkTypeId, int linkEntityId)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetAllLinksForLinkType(string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition GetLinkRuleDefinition(int id)
        {
            throw new NotImplementedException();
        }

        public List<LinkRuleDefinition> GetLinkRuleDefinitionsForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition GetLinkRuleDefinitionForEntityWithLinkTypeId(int entityId, string linkTypeId)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition AddLinkRuleDefinition(LinkRuleDefinition definition)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition UpdateLinkRuleDefinition(LinkRuleDefinition definition)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinkRuleDefinition(int id)
        {
            throw new NotImplementedException();
        }

        public List<LinkRuleDefinition> GetAllLinkRuleDefinitions()
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition DeleteAllRulesForLinkRuleDefinition(int definitionId)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition SetLinkRuleDefinitionRules(int definitionId, List<LinkRule> rules)
        {
            throw new NotImplementedException();
        }

        public LinkRuleDefinition SetLinkRuleDefinitionQueries(int definitionId, List<LinkRuleQuery> queries)
        {
            throw new NotImplementedException();
        }

        public void UpdateLinksForLinkRuleDefinitions()
        {
            throw new NotImplementedException();
        }

        public List<Entity> Search(Criteria critera, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> Search(Query query, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> Search(ComplexQuery query, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> LinkSearch(LinkQuery linkQuery, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> SystemSearch(SystemQuery systemQuery, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> SearchCompleteness(CompletenessQuery query, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> SearchSpecification(SpecificationQuery query, LoadLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Entity> QuickSearch(string searchString)
        {
            throw new NotImplementedException();
        }

        public List<SearchSuggestion> GetQuickSearchSuggestion(string prefix)
        {
            throw new NotImplementedException();
        }

        public List<TaskCategory> GetTasksAssignedToUser(string username, int maxAmount)
        {
            throw new NotImplementedException();
        }

        public List<TaskCategory> GetTasksCreatedByUser(string username, int maxAmount)
        {
            throw new NotImplementedException();
        }

        public List<TaskCategory> GetTasksByUserAndGroup(string username, string groupId, int maxAmount)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllCommentsForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Comment AddCommentWithResult(Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public bool EntityHasComment(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCommentsForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllSpecificationFieldTypes()
        {
            throw new NotImplementedException();
        }

        public List<SpecificationFieldType> GetAllSpecificationFieldTypes()
        {
            throw new NotImplementedException();
        }

        public SpecificationField UpdateSpecificationField(SpecificationField specificationField)
        {
            throw new NotImplementedException();
        }

        public void AddSpecificationField(SpecificationField field)
        {
            throw new NotImplementedException();
        }

        public SpecificationField GetSpecificationField(int entityId, string specificationFieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<SpecificationField> GetSpecificationFieldsForEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<SpecificationFieldType> GetSpecificationTemplateFieldTypes(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<SpecificationFieldType> GetSpecificationTemplateFieldTypesForFormatList(int entityId)
        {
            throw new NotImplementedException();
        }

        public SpecificationFieldType GetSpecificationFieldType(string specificationFieldTypeId)
        {
            throw new NotImplementedException();
        }

        public SpecificationFieldType AddSpecificationFieldType(SpecificationFieldType specFieldType)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecificationFieldType(string specificationFieldTypeId)
        {
            throw new NotImplementedException();
        }

        public SpecificationFieldType UpdateSpecificationFieldType(SpecificationFieldType specificationFieldType)
        {
            throw new NotImplementedException();
        }

        public void EnableSpecificationTemplateFieldType(bool enabled, int specificationTemplateId, string specificationFieldId)
        {
            throw new NotImplementedException();
        }

        public bool GetIsEnabledSpecificationTemplateFieldType(int specificationTemplateId, string specificationFieldId)
        {
            throw new NotImplementedException();
        }

        public string GetFormattedValue(SpecificationFieldType specificationFieldType, int entityId)
        {
            throw new NotImplementedException();
        }

        public string GetFormattedValueWithCultureInfo(SpecificationFieldType specificationFieldType, int entityId, CultureInfo ci)
        {
            throw new NotImplementedException();
        }

        public void CopySpecificationFields(int sourceEntityId, List<int> targetEntityIds)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllSpecificationCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetSpecificationCategory(string id)
        {
            throw new NotImplementedException();
        }

        public Category AddSpecificationCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category UpdateSpecificationCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSpecificationCategory(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllSpecificationCategories()
        {
            throw new NotImplementedException();
        }

        public string GetSpecificationAsHtml(int specificationEntityId, int entityId, CultureInfo ci)
        {
            throw new NotImplementedException();
        }

        public List<SpecificationFieldType> GetAllSpecificationFieldTypesForCategory(string categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
