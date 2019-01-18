using System;
using System.Collections.Generic;
using System.Globalization;
using inRiver.Remoting;
using inRiver.Remoting.Objects;

namespace InRiver.ImageAnalyzer.Tests
{
    internal class FakeModelService : IModelService
    {
        private readonly List<CVLValue> _cvlValues;

        public FakeModelService()
        {
            _cvlValues = new List<CVLValue>
            {
                {
                    new CVLValue
                    {
                        CVLId = "ResourceTags",
                        Key = "ball",
                        Value = "Ball"
                    }
                }
            };
        }

        public EntityType AddEntityType(EntityType entityType)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntityType(string id)
        {
            throw new NotImplementedException();
        }

        public List<EntityType> GetAllEntityTypes()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllEntityTypes()
        {
            throw new NotImplementedException();
        }

        public EntityType GetEntityType(string id)
        {
            throw new NotImplementedException();
        }

        public EntityType UpdateEntityType(EntityType entityType)
        {
            throw new NotImplementedException();
        }

        public List<EntityTypeStatistics> GetAllEntityTypeStatistics()
        {
            throw new NotImplementedException();
        }

        public LinkType AddLinkType(LinkType linkType)
        {
            throw new NotImplementedException();
        }

        public LinkType GetLinkType(string id)
        {
            throw new NotImplementedException();
        }

        public List<LinkType> GetAllLinkTypes()
        {
            throw new NotImplementedException();
        }

        public List<LinkType> GetLinkTypesForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllLinkTypes()
        {
            throw new NotImplementedException();
        }

        public bool DeleteLinkType(string id)
        {
            throw new NotImplementedException();
        }

        public LinkType UpdateLinkType(LinkType linkType)
        {
            throw new NotImplementedException();
        }

        public Category AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(string id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCategories()
        {
            throw new NotImplementedException();
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public CVL AddCVL(CVL cvl)
        {
            throw new NotImplementedException();
        }

        public CVL GetCVL(string id)
        {
            throw new NotImplementedException();
        }

        public List<CVL> GetAllCVLs()
        {
            throw new NotImplementedException();
        }

        public int GetCVLCount()
        {
            throw new NotImplementedException();
        }

        public bool DeleteCVL(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCVLs()
        {
            throw new NotImplementedException();
        }

        public CVL UpdateCVL(CVL cvl)
        {
            throw new NotImplementedException();
        }

        public CVLValue AddCVLValue(CVLValue cvlValue)
        {
            return cvlValue;
        }

        public bool DeleteCVLValue(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCVLValues()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCVLValuesForCVL(string cvlId)
        {
            throw new NotImplementedException();
        }

        public CVLValue GetCVLValue(int id)
        {
            throw new NotImplementedException();
        }

        public CVLValue GetCVLValueByKey(string key, string cvlId)
        {
            throw new NotImplementedException();
        }

        public List<CVLValue> GetCVLValuesForCVL(string cvlId, bool forceGet = false)
        {
            return _cvlValues;
        }

        public CVLValue UpdateCVLValue(CVLValue cvlValue)
        {
            throw new NotImplementedException();
        }

        public List<CVLValue> GetAllCVLValues()
        {
            throw new NotImplementedException();
        }

        public string GetCVLValueForLanguage(string cvlId, string cvlKey, CultureInfo ci)
        {
            throw new NotImplementedException();
        }

        public FieldView AddFieldView(FieldView fieldview)
        {
            throw new NotImplementedException();
        }

        public FieldView GetFieldView(string id)
        {
            throw new NotImplementedException();
        }

        public List<FieldView> GetAllFieldViews()
        {
            throw new NotImplementedException();
        }

        public FieldView UpdateFieldView(FieldView fieldView)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFieldView(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllFieldViews()
        {
            throw new NotImplementedException();
        }

        public bool DeleteFieldTypeFromFieldView(string fieldViewId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public bool AddFieldTypeToFieldView(string fieldViewId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<FieldView> GetFieldViewsForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFieldTypesForFieldView(string fieldViewId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFieldViewsForFieldType(string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public FieldSet AddFieldSet(FieldSet fieldSet)
        {
            throw new NotImplementedException();
        }

        public FieldSet GetFieldSet(string id)
        {
            throw new NotImplementedException();
        }

        public List<FieldSet> GetAllFieldSets()
        {
            throw new NotImplementedException();
        }

        public FieldSet UpdateFieldSet(FieldSet fieldSet)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFieldSet(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllFieldSets()
        {
            throw new NotImplementedException();
        }

        public bool DeleteFieldTypeFromFieldSet(string fieldSetId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public bool AddFieldTypeToFieldSet(string fieldSetId, string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public List<FieldSet> GetFieldSetsForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFieldTypesForFieldSet(string fieldSetId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFieldSetsForFieldType(string fieldTypeId)
        {
            throw new NotImplementedException();
        }

        public FieldType AddFieldType(FieldType fieldType)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFieldType(string id)
        {
            throw new NotImplementedException();
        }

        public List<FieldType> GetAllFieldTypes()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllFieldTypes()
        {
            throw new NotImplementedException();
        }

        public FieldType GetFieldType(string id)
        {
            throw new NotImplementedException();
        }

        public FieldType UpdateFieldType(FieldType fieldType)
        {
            throw new NotImplementedException();
        }

        public List<FieldType> GetFieldTypesForEntityTypeAndCategory(string entityTypeId, string categoryId)
        {
            throw new NotImplementedException();
        }

        public string ExportModelAsXmlString(bool includeCVLItems)
        {
            throw new NotImplementedException();
        }

        public bool ImportModelFromXmlString(string xml)
        {
            throw new NotImplementedException();
        }

        public CompletenessDefinition AddCompletenessDefinition(CompletenessDefinition definition)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCompletenessDefinitions()
        {
            throw new NotImplementedException();
        }

        public CompletenessDefinition UpdateCompletenessDefinition(CompletenessDefinition definition)
        {
            throw new NotImplementedException();
        }

        public CompletenessDefinition GetCompletenessDefinition(int id)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessDefinition> GetAllCompletenessDefinitions()
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompletenessDefinition(int id)
        {
            throw new NotImplementedException();
        }

        public void ReCalculateCompletenessForDefinition(int definitionId)
        {
            throw new NotImplementedException();
        }

        public CompletenessGroup AddCompletenessGroup(CompletenessGroup @group)
        {
            throw new NotImplementedException();
        }

        public CompletenessGroup UpdateCompletenessGroup(CompletenessGroup rule)
        {
            throw new NotImplementedException();
        }

        public CompletenessGroup GetCompletenessGroup(int id)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessGroup> GetCompletenessGroupForDefinition(int definitionId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompletenessGroup(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCompletenessGroups()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCompletenessGroupsForDefinition(int definitionId)
        {
            throw new NotImplementedException();
        }

        public CompletenessBusinessRule GetCompletenessBusinessRulesByGroupAndRule(int groupId, int ruleId)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessBusinessRule> GetCompletenessBusinessRulesForGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public CompletenessBusinessRule AddCompletenessBusinessRule(CompletenessBusinessRule rule)
        {
            throw new NotImplementedException();
        }

        public CompletenessBusinessRule ConnectExistingCompletenessBusinessRuleToNewGroup(CompletenessBusinessRule rule, int groupId)
        {
            throw new NotImplementedException();
        }

        public CompletenessBusinessRule UpdateCompletenessBusinessRuleForGroup(CompletenessBusinessRule rule, int groupId)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessBusinessRule> GetAllCompletenessBusinessRules()
        {
            throw new NotImplementedException();
        }

        public List<CompletenessRuleSetting> GetAllCompletenessRuleSettingsForBusinessRule(int businessRuleId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompletenessBusinessRuleForGroup(int ruleId, int groupId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllCompletenessBusinessRule()
        {
            throw new NotImplementedException();
        }

        public List<CompletenessCritera> GetAllCompletenessCriteras()
        {
            throw new NotImplementedException();
        }

        public CompletenessCritera GetCompletenessCriteraByType(string type)
        {
            throw new NotImplementedException();
        }

        public bool SetCompletenessBusinessRuleSettings(int ruleId, List<CompletenessRuleSetting> settings)
        {
            throw new NotImplementedException();
        }

        public CompletenessRuleSetting UpdatedCompletenessRuleSetting(int settingId, string value)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessAction> UpdateCompletenessActions(int definitionId, int ruleId, string trigger, List<CompletenessAction> actions)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessAction> GetCompletenessActionsByDefinitionIdRuleIdAndTrigger(int definitionid, int ruleid, string actiontrigger)
        {
            throw new NotImplementedException();
        }

        public List<CompletenessAction> GetCompletenessActionsByDefinitionIdGroupIdAndTrigger(int definitionid, int groupId, string actiontrigger)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompletenessAction(int id)
        {
            throw new NotImplementedException();
        }

        public CompletenessAction GetCompletenessAction(int id)
        {
            throw new NotImplementedException();
        }

        public CompletenessAction AddCompletenessAction(CompletenessAction action)
        {
            throw new NotImplementedException();
        }

        public CompletenessAction UpdateCompletenessAction(CompletenessAction action)
        {
            throw new NotImplementedException();
        }

        public EnvironmentLatestChangesSince GetEnvironmentLatestChanges(DateTime sinceUtcDt)
        {
            throw new NotImplementedException();
        }
    }
}
