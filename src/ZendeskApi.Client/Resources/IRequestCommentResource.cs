using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestCommentResource
    {
        Task<IResponse<TicketComment>> GetAsync(long id, long parentId);
        Task<IListResponse<TicketComment>> GetAllAsync(long parentId);
    }
}