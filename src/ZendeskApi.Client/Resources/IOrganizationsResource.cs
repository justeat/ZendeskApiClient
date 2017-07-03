using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationsResource
    {
        Task<IPagination<Organization>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<Organization>> GetAllByUserIdAsync(long userId, PagerParameters pager = null);
        Task<Organization> GetAsync(long organizationId);
        Task<IPagination<Organization>> GetAllAsync(long[] organizationIds, PagerParameters pager = null);
        Task<IPagination<Organization>> GetAllByExternalIdsAsync(string[] externalIds, PagerParameters pager = null);
        Task<Organization> CreateAsync(Organization organization);
        Task<Organization> UpdateAsync(Organization organization);
        Task DeleteAsync(long organizationId);
    }
}