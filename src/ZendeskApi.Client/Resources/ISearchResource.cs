using System.Threading.Tasks;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<IListResponse<T>> FindAsync<T>(IZendeskQuery<T> zendeskQuery);
    }
}