using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using inRiver.Remoting;
using inRiver.Remoting.Connect;
using inRiver.Remoting.Objects;
using inRiver.Remoting.Query;

namespace InRiver.ImageAnalyzer.Tests
{
    public class FakeUtilityService : IUtilityService
    {
        public bool SetServerSetting(string name, string value)
        {
            throw new NotImplementedException();
        }

        public string GetServerSetting(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteServerSetting(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetAllServerSettings()
        {
            throw new NotImplementedException();
        }

        public bool AddLanguage(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLanguage(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public List<CultureInfo> GetAllLanguages()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllLanguages()
        {
            throw new NotImplementedException();
        }

        public int AddFile(string fileName, byte[] data)
        {
            throw new NotImplementedException();
        }

        public int AddFileFromUrl(string fileName, string url)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllFiles()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFile(int id, string displayConfiguration)
        {
            return File.ReadAllBytes(@"C:\temp\stefan_holm_olsen.jpg");
            //return File.ReadAllBytes(@"C:\temp\stefan_holm_olsen.jpg");
        }

        public bool DeleteResourceFile(int id)
        {
            throw new NotImplementedException();
        }

        public ResourceFile GetFileMetaData(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllImageConfigurations()
        {
            throw new NotImplementedException();
        }

        public void ClearImageCache()
        {
            throw new NotImplementedException();
        }

        public void ClearImageCache(string configuration)
        {
            throw new NotImplementedException();
        }

        public void ClearImageCache(int resourceFileId)
        {
            throw new NotImplementedException();
        }

        public ResourceFile AddResourceFile(ResourceFile resourceFile)
        {
            throw new NotImplementedException();
        }

        public ResourceFile UpdateResourceFile(ResourceFile resourceFile)
        {
            throw new NotImplementedException();
        }

        public ResourceFile GetResourceFile(int id)
        {
            throw new NotImplementedException();
        }

        public List<Connector> GetAllConnectors()
        {
            throw new NotImplementedException();
        }

        public Connector AddConnector(Connector connector)
        {
            throw new NotImplementedException();
        }

        public Connector GetConnector(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConnector(string id)
        {
            throw new NotImplementedException();
        }

        public Connector SetConnectorSetting(string connectorId, string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConnectorSetting(string connectorId, string key)
        {
            throw new NotImplementedException();
        }

        public bool WriteConnectorEvent(ConnectorEvent connectorEvent)
        {
            throw new NotImplementedException();
        }

        public List<ConnectorEvent> GetConnectorEvents(string connectorId, int maxNumberOfEvents)
        {
            throw new NotImplementedException();
        }

        public Connector SetConnectorStarted(string connectorId, bool isStarted)
        {
            throw new NotImplementedException();
        }

        public List<ConnectorEvent> GetLatestConnectorEvents(int maxNumberOfEvents, bool onlyForChannel)
        {
            throw new NotImplementedException();
        }

        public List<UIPhrase> GetAllUIPhrases()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetAllUIPhrasesForLanguage(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public UIPhrase AddUIPhrase(UIPhrase phrase)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUIPhrase(string id)
        {
            throw new NotImplementedException();
        }

        public UIPhrase UpdateUIPhrase(UIPhrase phrase)
        {
            throw new NotImplementedException();
        }

        public List<CultureInfo> GetAllUILanguages()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllinRiverData()
        {
            throw new NotImplementedException();
        }

        public byte[] GetSmallIconForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public byte[] GetLargeIconForEntityType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, byte[]> GetAllEntityIcons()
        {
            throw new NotImplementedException();
        }

        public Task SendMail(string subject, string body, string toAddress, bool sendAsHtml)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder GetPersonalWorkAreaRootFolder(string username)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder GetPersonalWorkAreaFolder(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder AddPersonalWorkAreaFolder(WorkAreaFolder folder)
        {
            throw new NotImplementedException();
        }

        public bool DeletePersonalWorkAreaFolder(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdatePersonalWorkAreaFolderName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder MovePersonalWorkAreaFolder(Guid id, Guid newParentId, int newIndex)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdatePersonalWorkAreaQuery(Guid id, ComplexQuery query)
        {
            throw new NotImplementedException();
        }

        public List<WorkAreaFolder> GetAllPersonalWorkAreaFoldersForUser(string username, bool includeEntities)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdatePersonalWorkAreaFolderIndex(Guid id, int newIndex)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder GetSharedWorkAreaFolder(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder AddSharedWorkAreaFolder(WorkAreaFolder folder)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSharedWorkAreaFolder(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdateSharedWorkAreaSyndication(Guid id, bool isSyndication)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdateSharedWorkAreaFolderName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder MoveSharedWorkAreaFolder(Guid id, Guid newParentId, int newIndex)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdateSharedWorkAreaQuery(Guid id, ComplexQuery query)
        {
            throw new NotImplementedException();
        }

        public List<WorkAreaFolder> GetAllSharedWorkAreaFolders(bool includeEntities)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder UpdateSharedWorkAreaFolderIndex(Guid id, int newIndex)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder AddEntitiesToWorkAreaFolder(Guid id, List<int> ids)
        {
            throw new NotImplementedException();
        }

        public WorkAreaFolder RemoveEntitiesFromWorkAreaFolder(Guid id, List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Notification AddNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Notification UpdateNotificaton(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Notification GetNotification(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNotification(int id)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAllNotifications()
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAllNotificationsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAllActiveNotifications()
        {
            throw new NotImplementedException();
        }

        public FileImportMapping AddFileImportMapping(FileImportMapping mapping)
        {
            throw new NotImplementedException();
        }

        public FileImportMapping GetFileImportMapping(int id)
        {
            throw new NotImplementedException();
        }

        public FileImportMapping UpdateFileImportMapping(FileImportMapping mapping)
        {
            throw new NotImplementedException();
        }

        public List<FileImportMapping> GetAllFileImportMappings()
        {
            throw new NotImplementedException();
        }

        public List<FileImportMapping> GetAllFileImportMappingsByType(string entityTypeId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFileImportMapping(int id)
        {
            throw new NotImplementedException();
        }

        public List<ImageServiceConfiguration> GetAllImageServiceConfigurations()
        {
            throw new NotImplementedException();
        }

        public bool DeleteImageServiceConfiguration(int id)
        {
            throw new NotImplementedException();
        }

        public ImageServiceConfiguration AddImageServiceConfiguration(ImageServiceConfiguration imageServiceConfiguration)
        {
            throw new NotImplementedException();
        }

        public ImageServiceConfiguration UpdateImageServiceConfiguration(ImageServiceConfiguration imageServiceConfiguration)
        {
            throw new NotImplementedException();
        }

        public ImageServiceConfiguration GetImageServiceConfiguration(int id)
        {
            throw new NotImplementedException();
        }

        public List<PlannerView> GetAllPlannerViews()
        {
            throw new NotImplementedException();
        }

        public List<PlannerView> GetAllPlannerViewsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public PlannerView GetPlannerView(int id)
        {
            throw new NotImplementedException();
        }

        public PlannerView AddPlannerView(PlannerView view)
        {
            throw new NotImplementedException();
        }

        public PlannerView UpdatePlannerView(PlannerView view)
        {
            throw new NotImplementedException();
        }

        public bool DeletePlannerView(int id)
        {
            throw new NotImplementedException();
        }

        public string UpdateCalendarUrlForPlannerView(int id, string calendarUrl)
        {
            throw new NotImplementedException();
        }

        public string GetCalendarUrlForPlannerView(int id)
        {
            throw new NotImplementedException();
        }

        public PlannerView GetPlannerViewByCalendarUrl(string calendarUrl)
        {
            throw new NotImplementedException();
        }

        public List<ConnectorState> GetAllConnectorStates()
        {
            throw new NotImplementedException();
        }

        public List<ConnectorState> GetAllConnectorStatesForConnector(string connectorId)
        {
            throw new NotImplementedException();
        }

        public ConnectorState AddConnectorState(ConnectorState state)
        {
            throw new NotImplementedException();
        }

        public ConnectorState UpdateConnectorState(ConnectorState state)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConnectorState(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConnectorStates(string connectorId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConnectorStates(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllConnectorStates()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllHtmlTemplates()
        {
            throw new NotImplementedException();
        }

        public HtmlTemplate GetHtmlTemplate(int id)
        {
            throw new NotImplementedException();
        }

        public List<HtmlTemplate> GetAllHtmlTemplates(bool includeData = true)
        {
            throw new NotImplementedException();
        }

        public HtmlTemplate AddHtmlTemplate(HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public HtmlTemplate UpdateHtmlTemplate(HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHtmlTemplate(int id)
        {
            throw new NotImplementedException();
        }

        public void RebuildQuickSearchIndex()
        {
            throw new NotImplementedException();
        }
    }
}
