using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class SearchResource : ISearchResource
    {
        private const string SearchUri = @"/api/v2/search";

        private readonly IBaseClient _client;

        public SearchResource(IBaseClient client)
        {
            _client = client;
        }

        public IListResponse<T> Find<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity
        {
            var requestUri = _client.BuildUri(SearchUri, zendeskQuery.BuildQuery());

            return _client.Get<ListResponse<T>>(requestUri);
        }

        public IListResponse<T> FindAll<T>() where T : IZendeskEntity
        {
            var itemList = new List<T>();
            var items = new ListResponse<T>();
            var page = 1;
            while (itemList.Count < items.TotalCount || page == 1)
            {
                var zendeskQuery = new ZendeskQuery<T>().WithPaging(page, 100);
                var requestUri = _client.BuildUri(SearchUri, zendeskQuery.BuildQuery());
                items = (ListResponse<T>) _client.Get<ListResponse<T>>(requestUri);
                itemList.AddRange(items.Results);
                page++;
            }
            var itemListResponse = new ListResponse<T>
            {
                Results = itemList,
                TotalCount = itemList.Count
            };

            return itemListResponse;
        } 

    }
}
