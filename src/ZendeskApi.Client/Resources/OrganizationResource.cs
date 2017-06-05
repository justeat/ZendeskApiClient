using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationResource : ZendeskResource<Organization>, IOrganizationResource
    {
        private const string ResourceUri = "/api/v2/organizations";

        public OrganizationResource(ZendeskOptions options) : base(options) { }
        
        public async Task<IListResponse<Organization>> SearchByExtenalIdsAsync(params string[] externalIds)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(externalIds)}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationListResponse>();
            }
        }
        
        public async Task<IResponse<Organization>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationResponse>();
            }
        }
        
        public async Task<IResponse<Organization>> PutAsync(OrganizationRequest request)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationResponse>();
            }
        }
        
        public async Task<IResponse<Organization>> PostAsync(OrganizationRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationResponse>();
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
