using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestResource : ZendeskResource<Request>, IRequestResource
    {
        private const string ResourceUri = "/api/v2/requests";

        public RequestResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<Request>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<RequestResponse>();
            }
        }

        public async Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        {
            using (var client = CreateZendeskClient("/"))
            {
                // TODO: ngm make nicer
                var query = $"status={string.Join(",", requestedStatuses).ToLower()}";
                var response = await client.GetAsync($"{ResourceUri}?{query}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<RequestResponse>();
            }
        }

        public async Task<IResponse<Request>> PutAsync(RequestRequest request)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<RequestResponse>();
            }
        }

        public async Task<IResponse<Request>> PostAsync(RequestRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<RequestResponse>();
            }
        }

        public Task DeleteAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                return client.DeleteAsync(id.ToString());
            }
        }
    }
}
