using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestCommentResource
    {
        Task<TicketComment> GetAsync(long id, long parentId);
        Task<IListResponse<TicketComment>> GetAllAsync(long parentId);
    }
}