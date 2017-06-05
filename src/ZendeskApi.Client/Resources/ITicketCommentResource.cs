using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentResource
    {
        Task<IListResponse<TicketComment>> GetAllAsync(long parentId);
    }
}