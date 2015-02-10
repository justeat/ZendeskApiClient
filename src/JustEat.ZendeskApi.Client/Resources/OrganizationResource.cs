using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class OrganizationResource : ZendeskResource<Organization>, IOrganizationResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/organizations"; }
        }

        public OrganizationResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<Organization> Get(long id)
        {
            return Get<OrganizationResponse>(id);
        }

        public IResponse<Organization> Put(OrganizationRequest request)
        {
            return Put<OrganizationRequest, OrganizationResponse>(request);
        }

        public IResponse<Organization> Post(OrganizationRequest request)
        {
            return Post<OrganizationRequest, OrganizationResponse>(request);
        }
    }
}
