using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipResource
    {
        IListResponse<OrganizationMembership> GetAll(long id);
        IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request);
        IListResponse<OrganizationMembership> GetOrganizationMembers(long id);
    }
}