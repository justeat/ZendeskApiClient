using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationMembershipsResource
    {
        Task<IPagination<OrganizationMembership>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<OrganizationMembership>> GetAllForOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<IPagination<OrganizationMembership>> GetAllForUserAsync(long userId, PagerParameters pager = null);
        Task<OrganizationMembership> GetAsync(long id);
        Task<OrganizationMembership> GetForUserAndOrganizationAsync(long userId, long organizationId);
        Task<OrganizationMembership> CreateAsync(OrganizationMembership organizationMembership);
        Task<OrganizationMembership> PostForUserAsync(OrganizationMembership organizationMembership, string userId);
        Task<JobStatusResponse> CreateAsync(IEnumerable<OrganizationMembership> organizationMemberships);
        Task DeleteAsync(long organizationMembershipId);
        Task DeleteAsync(long userId, long organizationMembershipId);
    }
}