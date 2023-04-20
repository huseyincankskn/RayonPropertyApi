using Microsoft.Extensions.Configuration;
using Nest;
using System;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ElasticSearchOptions _options;

        public ElasticSearchService(IConfiguration configuration)
        {
            _options = configuration.GetSection("ElasticSearchOptions").Get<ElasticSearchOptions>();
            _elasticClient = GetClient();
        }

        private ElasticClient GetClient()
        {
            var connectionString = new ConnectionSettings(new Uri(_options.Url))
                .DisablePing()
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false)
                .BasicAuthentication(_options.Username, _options.Password)
                .DefaultIndex("paratic-log-*")
                .DefaultFieldNameInferrer(p => p);

            return new ElasticClient(connectionString);
        }

        public ISearchResponse<T> SearchLog<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : ElasticLogEntity, new()
        {
            return _elasticClient.Search(selector);
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class, new()
        {
            return _elasticClient.Search(selector);
        }
    }
}