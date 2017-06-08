using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipsResource
    {
        Task<IEnumerable<OrganizationMembership>> GetAllAsync();
        Task<IEnumerable<OrganizationMembership>> GetAllForOrganizationAsync(long organizationId);
        Task<IEnumerable<OrganizationMembership>> GetAllForUserAsync(long userId);
        Task<OrganizationMembership> GetAsync(long id);
        Task<OrganizationMembership> GetForUserAndOrganizationAsync(long userId, long organizationId);
        Task<OrganizationMembership> PostAsync(OrganizationMembership organizationMembership);
        Task<OrganizationMembership> PostForUserAsync(OrganizationMembership organizationMembership, string userId);
        Task<JobStatus> PostAsync(IEnumerable<OrganizationMembership> organizationMemberships);
        Task DeleteAsync(long organizationMembershipId);
        Task DeleteAsync(long userId, long organizationMembershipId);
    }
}