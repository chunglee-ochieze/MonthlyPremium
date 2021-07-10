using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;

namespace MonthlyPremiumUtilities
{
    public class Logger
    {
        private readonly ConfigMgr _configMgr;
        public Logger()
        {
            _configMgr = new ConfigMgr();
        }

        public void Information(string message, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Information, null, caller);
        }

        public void Warning(string message, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Warning, null, caller);
        }

        public void Error(string message, Exception exception = null, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Error, exception, caller);
        }

        public void Fatal(string message, Exception exception = null, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Fatal, exception, caller);
        }

        public void Verbose(string message, Exception exception = null, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Verbose, exception, caller);
        }

        public void Debug(string message, Exception exception = null, [CallerMemberName] string caller = null)
        {
            Log(message, LogEventLevel.Debug, exception, caller);
        }

        private void Log(string message, LogEventLevel logType, Exception exception = null, [CallerMemberName] string caller = null)
        {
            try
            {
                var logger = _configMgr.Logger();
                var path = Path.Combine(logger.LogPath, $"Log_{logType}\\{logType.ToString().ToUpper()}-{DateTime.Now:yy-MM-dd}.txt");

                message = $"\r\nMethod: {caller}\r\nMessage: {message}\r\n_______________________________________";

                using var log = new LoggerConfiguration()
                    .WriteTo.File(path, shared: true, rollOnFileSizeLimit: true, fileSizeLimitBytes: logger.LogSize)
                    .CreateLogger();

                switch (logType)
                {
                    case LogEventLevel.Information:
                        log.Information(message);
                        break;
                    case LogEventLevel.Warning:
                        log.Warning(message);
                        break;
                    case LogEventLevel.Error:
                        log.Error(exception, message);
                        break;
                    case LogEventLevel.Fatal:
                        log.Fatal(exception, message);
                        break;
                    case LogEventLevel.Verbose:
                        log.Verbose(exception, message);
                        break;
                    case LogEventLevel.Debug:
                        log.Debug(exception, message);
                        break;
                    default:
                        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            return;

                        var evl = new EventLog
                        {
                            Source = "MonthlyPremium"
                        };
                        evl.WriteEntry(message, EventLogEntryType.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return;

                var evl = new EventLog
                {
                    Source = "MonthlyPremium"
                };
                evl.WriteEntry($"Error encountered in: {MethodBase.GetCurrentMethod()?.Name}");
                evl.WriteEntry($"Error: {ex.Message}\r\nDetail: {ex.StackTrace}", EventLogEntryType.Error);
                evl.WriteEntry($"\r\nIntended Log:\r\nLogLevel: {logType}\r\nMethod: {caller}\r\nMessage: {message}\r\n_______________________________________\r\n");
            }
        }
    }
}
