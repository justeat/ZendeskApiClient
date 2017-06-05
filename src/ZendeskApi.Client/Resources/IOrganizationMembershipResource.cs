using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipResource
    {
        Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId);
        Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId);
        Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request);
    }
}