using Core.Utilities.Security.Jwt;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public class ElasticLogEntity : ElasticBaseLogEntity
    {
        public object Data { get; set; }
        public TokenUserInfo User { get; set; }
        public LogType LogType { get; set; }
        public string Application { get; set; }
        public string IPAddress { get; set; }
    }

    public class ElasticLogEntity<TData> : ElasticLogEntity
    {
        new public TData Data { get; set; }
    }
}