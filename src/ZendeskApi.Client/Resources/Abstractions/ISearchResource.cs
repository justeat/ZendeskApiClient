using System.Threading.Tasks;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<IListResponse<T>> Search<T>(IZendeskQuery<T> zendeskQuery);
    }
}