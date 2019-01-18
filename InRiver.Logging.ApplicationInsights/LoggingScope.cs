using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace InRiver.Logging.ApplicationInsights
{
    public class LoggingScope : IDisposable
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IOperationHolder<RequestTelemetry> _operationHolder;

        public LoggingScope(
            string action,
            Dictionary<string, string> settings)
        {
            if (!settings.TryGetValue(SettingsConstants.DisableTelemetry, out string disableTelemetry) ||
                !settings.TryGetValue(SettingsConstants.InstrumentationKey, out string instrumentationKey))
            {
                return;
            }

            if (string.Equals(disableTelemetry, "true", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(instrumentationKey))
            {
                return;
            }

            var config = new TelemetryConfiguration
            {
                DisableTelemetry = string.Equals(disableTelemetry, "true", StringComparison.OrdinalIgnoreCase),
                InstrumentationKey = instrumentationKey
            };

            _telemetryClient = new TelemetryClient(config);
            _operationHolder = _telemetryClient.StartOperation<RequestTelemetry>(action);
        }

        public DependencyScope CreateDependencyTracker(
            string dependencyType,
            string dependencyName,
            string command)
        {
            return new DependencyScope(
                _telemetryClient,
                _operationHolder.Telemetry,
                dependencyType,
                dependencyName,
                command);
        }

        public void TrackEvent(
            string eventName,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            var telemetry = new EventTelemetry(eventName);
            telemetry.Context.Operation.ParentId = _operationHolder.Telemetry.Id;
            telemetry.Context.Operation.Id = _operationHolder.Telemetry.Context.Operation.Id;
            telemetry.Timestamp = DateTimeOffset.UtcNow;

            DictionaryHelpers.CopyDictionary(metrics, telemetry.Metrics);
            DictionaryHelpers.CopyDictionary(properties, telemetry.Properties);

            _telemetryClient.TrackEvent(telemetry);
        }

        public void TrackException(
            Exception exception,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            var telemetry = new ExceptionTelemetry(exception);
            telemetry.Context.Operation.ParentId = _operationHolder.Telemetry.Id;
            telemetry.Context.Operation.Id = _operationHolder.Telemetry.Context.Operation.Id;
            telemetry.Timestamp = DateTimeOffset.UtcNow;

            DictionaryHelpers.CopyDictionary(metrics, telemetry.Metrics);
            DictionaryHelpers.CopyDictionary(properties, telemetry.Properties);

            _telemetryClient.TrackException(telemetry);
        }

        public void Dispose()
        {
            _telemetryClient.StopOperation(_operationHolder);
            _telemetryClient.Flush();
        }
    }
}
