using Nest;
using System;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public interface IElasticSearchService
    {
        ISearchResponse<T> SearchLog<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : ElasticLogEntity, new();

        ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class, new();
    }
}