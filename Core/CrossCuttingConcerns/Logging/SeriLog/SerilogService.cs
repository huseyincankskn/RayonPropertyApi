using Core.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Context;
using System;

namespace Core.CrossCuttingConcerns.Logging.SeriLog
{
    public class SerilogService : ISeriLogService
    {
        private readonly ILogger _logger;
        private readonly IHttpAccessorHelper _httpAccessorHelper;

        public SerilogService(ILogger<SerilogService> logger, IHttpAccessorHelper httpAccessorHelper)
        {
            _logger = logger;
            _httpAccessorHelper = httpAccessorHelper;
        }

        public void Info(object logMessage)
        {
            string jsonLog = JsonConvert.SerializeObject(logMessage);
            _logger.LogInformation(LogEvent.GetItem, $"{jsonLog}");
        }

        public void Info(object data, LogType logType, int logEventId = LogEvent.GetItem)
        {
            using (LogContext.PushProperty("LogType", (int)logType))
            using (LogContext.PushProperty("Data", data, true))
            using (LogContext.PushProperty("User", _httpAccessorHelper.GetUserInfo(), true))
            {
                _logger.LogInformation(logEventId, "logtype: {LogTypeName}", logType);
            }
        }

        public void Debug(object logMessage)
        {
            string jsonLog = JsonConvert.SerializeObject(logMessage);
            _logger.LogDebug(LogEvent.GetItem, $"{jsonLog}");
        }

        public void Debug(object data, LogType logType, int logEventId = LogEvent.GetItem)
        {
            using (LogContext.PushProperty("LogType", (int)logType))
            using (LogContext.PushProperty("Data", data, true))
            using (LogContext.PushProperty("User", _httpAccessorHelper.GetUserInfo(), true))
            {
                _logger.LogDebug(logEventId, "logtype: {LogTypeName}", logType);
            }
        }

        public void Warning(object logMessage)
        {
            string jsonLog = JsonConvert.SerializeObject(logMessage);
            _logger.LogWarning(LogEvent.GetItem, $"{jsonLog}");
        }

        public void Warning(object data, LogType logType, int logEventId = LogEvent.GetItem)
        {
            using (LogContext.PushProperty("LogType", (int)logType))
            using (LogContext.PushProperty("Data", data, true))
            using (LogContext.PushProperty("User", _httpAccessorHelper.GetUserInfo(), true))
            {
                _logger.LogWarning(logEventId, "logtype: {LogTypeName}", logType);
            }
        }

        public void Error(object logMessage)
        {
            string jsonLog = JsonConvert.SerializeObject(logMessage);
            _logger.LogError(LogEvent.GetItem, $"{jsonLog}");
        }

        public void Error(object data, LogType logType, int logEventId = LogEvent.GetItem)
        {
            using (LogContext.PushProperty("LogType", (int)logType))
            using (LogContext.PushProperty("Data", data, true))
            using (LogContext.PushProperty("User", _httpAccessorHelper.GetUserInfo(), true))
            {
                _logger.LogError(logEventId, "logtype: {LogTypeName}", logType);
            }
        }

        public void Error(object data, Exception e, LogType logType, int logEventId = LogEvent.GetItem)
        {
            using (LogContext.PushProperty("LogType", (int)logType))
            using (LogContext.PushProperty("Data", data, true))
            using (LogContext.PushProperty("User", _httpAccessorHelper.GetUserInfo(), true))
            {
                _logger.LogError(logEventId, e, "logtype: {LogTypeName}", logType);
            }
        }
    }
}