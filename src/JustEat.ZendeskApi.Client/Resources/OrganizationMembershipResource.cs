using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : ZendeskResource<OrganizationMembership>, IOrganizationMembershipResource
    {
        protected override string ResourceUri {
            //get { return @"/api/v2/users/{0}/organization_memberships"; }
            get { return @"/api/v2/organizations/32144722/memberships"; }
        }

        public OrganizationMembershipResource(IBaseClient client)
        {
            Client = client;
        }

        public IListResponse<OrganizationMembership> GetAll(long id)
        {
            return GetAll<OrganizationMembershipListResponse>(id);
        }

        public IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request)
        {
            return Post<OrganizationMembershipRequest, OrganizationMembershipResponse>(request, request.Item.UserId);
        }
    }
}
