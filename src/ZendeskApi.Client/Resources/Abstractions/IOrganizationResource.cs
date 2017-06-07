using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

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