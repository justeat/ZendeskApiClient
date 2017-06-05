using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : ZendeskResource<TicketComment>, IRequestCommentResource
    {
        private const string ResourceUri = "/api/v2/requests/{0}/comments";

        public RequestCommentResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<TicketComment>> GetAsync(long id, long parentId)
        {
            using (var client = CreateZendeskClient(string.Format(ResourceUri, parentId) + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketCommentResponse>();
            }
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(string.Format(ResourceUri, parentId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketCommentListResponse>();
            }
        }
    }
}
