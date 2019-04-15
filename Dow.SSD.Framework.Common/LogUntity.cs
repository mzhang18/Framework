using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Configuration;
using System.Web;

namespace Dow.SSD.Framework.Common
{
    public static class LogUntity
    {
        static string projectTitle = ConfigurationManager.AppSettings["WebSiteTitle"].Trim().ToString();

        public static void LogError(Exception ex, params string[] moreInfo)
        {
            if (Logger.IsLoggingEnabled())
            {
                try
                {
                    LogEntry log = GetLogEntry();
                    log.Categories.Add(projectTitle);
                    log.Priority = 9;
                    log.Severity = System.Diagnostics.TraceEventType.Error;
                    log.Message = ex.Message + " : " + String.Join(" ", moreInfo);
                    log.ExtendedProperties.Add("Stack Trace", ex.StackTrace);
                    log.ExtendedProperties.Add("Target Site", ex.TargetSite);
                    Logger.Write(log);
                }
                catch { }
            }
        }

        public static void LogMessage(string message)
        {
            if (Logger.IsLoggingEnabled())
            {
                try
                {
                    LogEntry log = GetLogEntry();
                    log.Categories.Add(projectTitle);
                    log.Priority = 5;
                    log.Severity = System.Diagnostics.TraceEventType.Information;
                    log.Message = message;
                    Logger.Write(log);
                }
                catch { }
            }
        }

        private static LogEntry GetLogEntry()
        {
            LogEntry log = new LogEntry();
            log.TimeStamp = DateTime.Now;
            log.Title = projectTitle;
            log.Priority = 5;                       // default priority
            log.Severity = System.Diagnostics.TraceEventType.Verbose;  // default severity
            log.MachineName = HttpContext.Current.Server.MachineName;
            log.ProcessId = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            return log;
        }

    }
}
