using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationsResource
    {
        Task<IPagination<Organization>> GetAllAsync();
        Task<IPagination<Organization>> GetAllByUserIdAsync(long userId);
        Task<Organization> GetAsync(long organizationId);
        Task<IPagination<Organization>> GetAllAsync(long[] organizationIds);
        Task<IPagination<Organization>> GetAllByExternalIdsAsync(string[] externalIds);
        Task<Organization> PostAsync(Organization organization);
        Task<Organization> PutAsync(Organization organization);
        Task DeleteAsync(long organizationId);
    }
}