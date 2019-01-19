using System.Collections.Generic;
using inRiver.Remoting.Extension;
using Xunit;
using Xunit.Abstractions;

namespace InRiver.Logging.ApplicationInsights.Tests
{
    public class DummyEntityListenerTests
    {
        private readonly DummyEntityListener _subject;

        public DummyEntityListenerTests(
            ITestOutputHelper testOutputHelper)
        {
            _subject = new DummyEntityListener
            {
                Context = new inRiverContext(
                    null,
                    new XUnitLogger(testOutputHelper))
            };
        }

        [Fact]
        public void RunTestMethod()
        {
            // Arrange
            _subject.Context.Settings = new Dictionary<string, string>
            {
                {SettingsConstants.DisableTelemetry, "FALSE"},
                {SettingsConstants.InstrumentationKey, "REPLACE ME"}
            };

            // Act
            string result = _subject.Test();

            // Assert
            Assert.StartsWith("SUCCESS:", result);
        }
    }
}
