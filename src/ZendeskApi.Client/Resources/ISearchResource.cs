using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<SearchResponse<ISearchResponse>> SearchAsync(Action<IZendeskQuery> builder, PagerParameters pager = null);
        Task<SearchResponse<T>> SearchAsync<T>(Action<IZendeskQuery> builder, PagerParameters pager = null) where T : ISearchResponse;
    }
}