using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<SearchListResponse<ISearchResult>> SearchAsync(Action<IZendeskQuery> builder, PagerParameters pager = null);
        Task<SearchListResponse<T>> SearchAsync<T>(Action<IZendeskQuery> builder, PagerParameters pager = null) where T : ISearchResult;
    }
}