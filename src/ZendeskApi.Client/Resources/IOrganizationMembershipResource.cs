using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipResource
    {
        IListResponse<OrganizationMembership> GetAllByOrganization(long organizationId);
        IListResponse<OrganizationMembership> GetAllByUser(long userId);
        IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request);
        IListResponse<OrganizationMembership> GetAll(long id);
    }
}