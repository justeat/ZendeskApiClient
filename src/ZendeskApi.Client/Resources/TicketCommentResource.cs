using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentResource : ZendeskResource<TicketComment>, ITicketCommentResource
    {
        private const string ResourceUri = "/api/v2/tickets/{0}/comments";

        public TicketCommentResource(IRestClient client)
        {
            Client = client;
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAll<TicketCommentListResponse>(string.Format(ResourceUri, parentId));
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            return await GetAllAsync<TicketCommentListResponse>(string.Format(ResourceUri, parentId)).ConfigureAwait(false);
        }
    }
}
