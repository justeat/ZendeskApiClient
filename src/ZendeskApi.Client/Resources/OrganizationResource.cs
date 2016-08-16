using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationResource : ZendeskResource<Organization>, IOrganizationResource
    {
        protected override string ResourceUri => @"/api/v2/organizations";

        public OrganizationResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Organization> Get(long id)
        {
            return GetAsync(id).Result;
        }

        public async Task<IResponse<Organization>> GetAsync(long id)
        {
            return await GetAsync<OrganizationResponse>(id).ConfigureAwait(false);
        }

        public IResponse<Organization> Put(OrganizationRequest request)
        {
            return PutAsync(request).Result;
        }

        public async Task<IResponse<Organization>> PutAsync(OrganizationRequest request)
        {
            return await PutAsync<OrganizationRequest, OrganizationResponse>(request).ConfigureAwait(false);
        }

        public IResponse<Organization> Post(OrganizationRequest request)
        {
            return PostAsync(request).Result;
        }

        public async Task<IResponse<Organization>> PostAsync(OrganizationRequest request)
        {
            return await PostAsync<OrganizationRequest, OrganizationResponse>(request).ConfigureAwait(false);
        }
    }
}
