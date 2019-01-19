using System;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Log;
using Xunit.Abstractions;

namespace InRiver.Logging.ApplicationInsights.Tests
{
    public class XUnitLogger : IExtensionLog
    {
        private readonly ITestOutputHelper _outputHelper;

        public XUnitLogger(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void Log(LogLevel level, string message)
        {
            _outputHelper.WriteLine($"{level}: {message}");
        }

        public void Log(LogLevel level, string message, Exception ex)
        {
            _outputHelper.WriteLine($"{level}: {message}. Exception: {ex}");
        }
    }
}
