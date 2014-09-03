using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipResource
    {
        IListResponse<OrganizationMembership> GetAll(long id);
        IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request);
    }
}