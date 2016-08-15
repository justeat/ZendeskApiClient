using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestResource : ZendeskResource<Request>, IRequestResource
    {
        protected override string ResourceUri => @"/api/v2/requests";

        public RequestResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Request> Get(long id)
        {
            return Get<RequestResponse>(id);
        }

        public async Task<IResponse<Request>> GetAsync(long id)
        {
            return await GetAsync<RequestResponse>(id);
        }

        public IResponse<Request> Get(IEnumerable<TicketStatus> requestedStatuses)
        {
            return Get<RequestResponse>($"status={string.Join(",", requestedStatuses).ToLower()}");
        }

        public async Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        {
            return await GetAsync<RequestResponse>($"status={string.Join(",", requestedStatuses).ToLower()}");
        }

        public IResponse<Request> Put(RequestRequest request)
        {
            return Put<RequestRequest, RequestResponse>(request);
        }

        public async Task<IResponse<Request>> PutAsync(RequestRequest request)
        {
            return await PutAsync<RequestRequest, RequestResponse>(request);
        }

        public IResponse<Request> Post(RequestRequest request)
        {
            return Post<RequestRequest, RequestResponse>(request);
        }

        public async Task<IResponse<Request>> PostAsync(RequestRequest request)
        {
            return await PostAsync<RequestRequest, RequestResponse>(request);
        }
    }
}
