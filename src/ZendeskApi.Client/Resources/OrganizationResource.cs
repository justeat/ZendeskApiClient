using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationResource : ZendeskResource<Organization>, IOrganizationResource
    {
        private const string ResourceUri = "/api/v2/organizations";
        private const string ResourceUriSearch = "/api/v2/organizations/show_many";

        public OrganizationResource(IRestClient client)
        {
            Client = client;
        }
 
        public IListResponse<Organization> SearchByExtenalIds(params string[] externalIds)
        {
            return GetAllByExtenalId<OrganizationListResponse>(ResourceUriSearch, externalIds);
        }

        public async Task<IListResponse<Organization>> SearchByExtenalIdsAsync(params string[] externalIds)
        {
            return await GetAllByExtenalIdAsync<OrganizationListResponse>(ResourceUriSearch, externalIds);
        }

        public IResponse<Organization> Get(long id)
        {
            string url = $"{ResourceUri}/{id}";
            return Get<OrganizationResponse>(url);
        }

        public async Task<IResponse<Organization>> GetAsync(long id)
        {
            string url = $"{ResourceUri}/{id}";
            return await GetAsync<OrganizationResponse>(url).ConfigureAwait(false);
        }

        public IResponse<Organization> Put(OrganizationRequest request)
        {
            ValidateRequest(request);

            string url = $"{ResourceUri}/{request.Item.Id}";
            return Put<OrganizationRequest, OrganizationResponse>(request, url);
        }

        public async Task<IResponse<Organization>> PutAsync(OrganizationRequest request)
        {
            ValidateRequest(request);

            string url = $"{ResourceUri}/{request.Item.Id}";
            return await PutAsync<OrganizationRequest, OrganizationResponse>(request, url).ConfigureAwait(false);
        }

        public IResponse<Organization> Post(OrganizationRequest request)
        {
            return Post<OrganizationRequest, OrganizationResponse>(request, ResourceUri);
        }

        public async Task<IResponse<Organization>> PostAsync(OrganizationRequest request)
        {
            return await PostAsync<OrganizationRequest, OrganizationResponse>(request, ResourceUri).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            ValidateRequest(id);
            await DeleteAsync($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public void Delete(long id)
        {
            ValidateRequest(id);
            Delete($"{ResourceUri}/{id}");
        }


    }
}
