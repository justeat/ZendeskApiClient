using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentResource : ZendeskResource<TicketComment>, ITicketCommentResource
    {
        private const string ResourceUri = "/api/v2/tickets/{0}/comments";

        public TicketCommentResource(ZendeskOptions options) : base(options) { }

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
