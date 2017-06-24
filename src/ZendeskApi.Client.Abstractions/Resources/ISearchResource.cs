using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<IPagination<SearchResult>> SearchAsync<T>(IZendeskQuery<T> zendeskQuery);
    }
}