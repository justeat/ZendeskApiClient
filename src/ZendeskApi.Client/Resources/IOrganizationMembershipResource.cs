using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipResource
    {
        Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId);
        IListResponse<OrganizationMembership> GetAllByUser(long userId);
        Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId);
        IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request);
        Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request);
        IListResponse<OrganizationMembership> GetAll(long id);
    }
}