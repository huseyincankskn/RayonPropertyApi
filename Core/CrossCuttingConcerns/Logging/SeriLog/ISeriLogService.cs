using System;

namespace Core.CrossCuttingConcerns.Logging.SeriLog
{
    public interface ISeriLogService
    {
        public void Info(object logMessage);

        void Info(object data, LogType logType, int logEventId = LogEvent.GetItem);

        public void Debug(object logMessage);

        void Debug(object data, LogType logType, int logEventId = LogEvent.GetItem);

        public void Warning(object logMessage);

        void Warning(object data, LogType logType, int logEventId = LogEvent.GetItem);

        public void Error(object logMessage);

        void Error(object data, LogType logType, int logEventId = LogEvent.GetItem);

        void Error(object data, Exception e, LogType logType, int logEventId = LogEvent.GetItem);
    }
}