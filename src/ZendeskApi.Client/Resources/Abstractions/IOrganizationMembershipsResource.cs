using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipsResource
    {
        Task<IEnumerable<OrganizationMembership>> GetAllAsync();
        Task<IEnumerable<OrganizationMembership>> GetAllForOrganizationAsync(long organizationId);
        Task<IEnumerable<OrganizationMembership>> GetAllForUserAsync(long userId);
        Task<OrganizationMembership> GetAsync(long id);
        Task<OrganizationMembership> GetForUserAndOrganizationAsync(long userId, long organizationId);
        Task<OrganizationMembership> PostAsync(OrganizationMembershipRequest request);
        Task<OrganizationMembership> PostForUserAsync(OrganizationMembershipRequest request, string userId);
    }
}