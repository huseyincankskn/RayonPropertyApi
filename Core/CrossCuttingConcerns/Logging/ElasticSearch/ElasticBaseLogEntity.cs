using Nest;
using System;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public class ElasticBaseLogEntity
    {
        [PropertyName("@timestamp")]
        public DateTime Date { get; set; }

        [PropertyName("message")]
        public string Message { get; set; }

        [PropertyName("messageTemplate")]
        public string MessageTemplate { get; set; }

        [PropertyName("level")]
        public string Level { get; set; }

        [PropertyName("exception")]
        public Exception Exception { get; set; }
    }
}