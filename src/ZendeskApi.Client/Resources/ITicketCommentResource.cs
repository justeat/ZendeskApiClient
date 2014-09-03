using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentResource
    {
        IListResponse<TicketComment> GetAll(long parentId);
    }
}