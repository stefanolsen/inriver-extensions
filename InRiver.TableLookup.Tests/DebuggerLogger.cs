using System;
using System.Diagnostics;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Log;

namespace InRiver.TableLookup.Tests
{
    public class DebuggerLogger : IExtensionLog
    {
        public void Log(LogLevel level, string message)
        {
            Debug.WriteLine($"{level}: {message}");
        }

        public void Log(LogLevel level, string message, Exception ex)
        {
            Debug.WriteLine($"{level}: {message}. Exception: {ex}");
        }
    }
}
