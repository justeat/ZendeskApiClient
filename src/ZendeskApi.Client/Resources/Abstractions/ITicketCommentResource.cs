using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentResource
    {
        Task<IListResponse<TicketComment>> GetAllAsync(long parentId);
    }
}