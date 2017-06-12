using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationsResource
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<IEnumerable<Organization>> GetAllByUserIdAsync(long userId);
        Task<Organization> GetAsync(long organizationId);
        Task<IEnumerable<Organization>> GetAllAsync(long[] organizationIds);
        Task<IEnumerable<Organization>> GetAllByExternalIdsAsync(string[] externalIds);
        Task<Organization> PostAsync(Organization organization);
        Task<Organization> PutAsync(Organization organization);
        Task DeleteAsync(long organizationId);
    }
}