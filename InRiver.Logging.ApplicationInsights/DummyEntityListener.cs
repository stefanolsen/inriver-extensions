using System;
using System.Collections.Generic;
using System.Threading;
using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Log;
using inRiver.Remoting.Objects;

namespace InRiver.Logging.ApplicationInsights
{
    public class DummyEntityListener : IEntityListener
    {
        private LoggingScope _loggingScope;

        public inRiverContext Context { get; set; }
        public Dictionary<string, string> DefaultSettings => new Dictionary<string, string>
        {
            { SettingsConstants.DisableTelemetry, "TRUE" },
            { SettingsConstants.InstrumentationKey, "REPLACE ME" }
        };

        public string Test()
        {
            _loggingScope = new LoggingScope("DummyEntityListener.Test", Context.Settings);
            try
            {
                LongRunningOperation();

                return "SUCCESS: Self-tests completed.";
            }
            catch (Exception ex)
            {
                // Catch and log unhandled exceptions.
                Context.Log(LogLevel.Error, "Unhandled exception in Test method.", ex);
                _loggingScope.TrackException(ex);

                return "ERROR: Something failed.";
            }
            finally
            {
                // Flush all unsent entries form the buffer.
                _loggingScope.Dispose();
            }
        }

        private void LongRunningOperation()
        {
            // Log a regular trace message.
            Context.Log(LogLevel.Information, "Starting a long running.");
            _loggingScope.TrackEvent("Starting a long running.");

            LoadEntities("Product");
            LoadEntities("Item");

            // Log a regular trace message.
            Context.Log(LogLevel.Information, "Finished a long running.");
            _loggingScope.TrackEvent("Finished a long running.");
        }

        private void LoadEntities(string entityType)
        {
            using (DependencyScope dependencyTracker =
                _loggingScope.CreateDependencyTracker(
                    DependencyType.WcfService,
                    DependencyNames.DataService,
                    nameof(IDataService.GetAllEntitiesForEntityType)))
            {
                //ICollection<Entity> allProducts = Context.ExtensionManager.DataService
                //    .GetAllEntitiesForEntityType(
                //        entityType,
                //        LoadLevel.DataOnly);
                //dependencyTracker.IsSuccessful = true;

                // Simulate load time, instead of running above method call.
                Thread.Sleep(1021);

                // Set the 'IsSuccessful' property to 'true', so it appears properly in log views.
                // Default value is 'false'.
                dependencyTracker.IsSuccessful = true;
            }
        }

        public void EntityCreated(int entityId)
        {
        }

        public void EntityUpdated(int entityId, string[] fields)
        {
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
    }
}
