using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Queries;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<IPagination<ISearchResult>> SearchAsync(Action<IZendeskQuery> builder, PagerParameters pager = null);

        Task<IPagination<T>> SearchAsync<T>(Action<IZendeskQuery> builder, PagerParameters pager = null) where T : ISearchResult;
    }
}