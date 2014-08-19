using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : ZendeskResource<OrganizationMembership>, IOrganizationMembershipResource
    {

        private const string UsersResourceUrl = @"/api/v2/users/{0}/organization_memberships";
        private const string OrganizationsResourceUrl = @"/api/v2/organizations/{0}/organization_memberships";
        private string _resourceUrl = "";

        protected override string ResourceUri {
            get { return _resourceUrl; }
        }

        public OrganizationMembershipResource(IBaseClient client)
        {
            Client = client;
        }

        public IListResponse<OrganizationMembership> GetAll(long id)
        {
            _resourceUrl = UsersResourceUrl;
            return GetAll<OrganizationMembershipListResponse>(id);
        }

        public IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request)
        {
            _resourceUrl = UsersResourceUrl;
            return Post<OrganizationMembershipRequest, OrganizationMembershipResponse>(request, request.Item.UserId);
        }

        public IListResponse<OrganizationMembership> GetOrganizationMembers(long id)
        {
            _resourceUrl = OrganizationsResourceUrl;
            return GetAll<OrganizationMembershipListResponse>(id);
        } 
    }
}
