using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace InRiver.Logging.ApplicationInsights
{
    public class DependencyScope : IDisposable
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly RequestTelemetry _parentTelemetry;

        private readonly string _dependencyType;
        private readonly string _dependencyName;
        private readonly string _command;
        private readonly DateTimeOffset _startTime;

        public bool IsSuccessful { get; set; }

        public DependencyScope(
            TelemetryClient telemetryClient,
            RequestTelemetry parentTelemetry,
            string dependencyType,
            string dependencyName,
            string command)
        {
            _telemetryClient = telemetryClient;
            _parentTelemetry = parentTelemetry;
            _dependencyType = dependencyType;
            _dependencyName = dependencyName;
            _command = command;

            _startTime = DateTimeOffset.UtcNow;
        }

        public void Dispose()
        {
            TimeSpan duration = DateTimeOffset.UtcNow.Subtract(_startTime);

            var telemetry = new DependencyTelemetry(
                _dependencyType,
                null,
                _dependencyName,
                _command,
                _startTime,
                duration,
                null,
                IsSuccessful);

            telemetry.Context.Operation.ParentId = _parentTelemetry.Id;
            telemetry.Context.Operation.Id = _parentTelemetry.Context.Operation.Id;
            telemetry.Timestamp = DateTimeOffset.UtcNow;

            _telemetryClient.TrackDependency(telemetry);
        }
    }
}
