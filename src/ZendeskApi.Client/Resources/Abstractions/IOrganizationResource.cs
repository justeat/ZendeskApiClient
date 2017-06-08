using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationResource
    {
        Task DeleteAsync(long id);
        Task<IListResponse<Organization>> SearchByExtenalIdsAsync(params string[] externalIds);
        Task<Organization> GetAsync(long id);
        Task<Organization> PutAsync(OrganizationRequest request);
        Task<Organization> PostAsync(OrganizationRequest request);
    }
}