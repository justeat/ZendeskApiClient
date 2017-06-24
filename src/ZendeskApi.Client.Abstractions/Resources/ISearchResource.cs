using System.Threading.Tasks;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        Task<IListResponse<T>> SearchAsync<T>(IZendeskQuery<T> zendeskQuery);
    }
}