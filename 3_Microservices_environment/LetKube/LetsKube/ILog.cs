using NLog;
using System;

namespace LetsKube
{
    public interface ILog
    {
        void Information(string message);
    }

    public class LogNLog : ILog
    {
        private ILogger _logger;
        public LogNLog(ILogger logger)
        {
            _logger = logger;
        }

        public void Information(string message)
        {
            var logEventInfo = new LogEventInfo(LogLevel.Info, "RmqLogMessage", $"{message}, generated at {DateTime.UtcNow}.");
            _logger.Log(logEventInfo); 
        }
    }
}
