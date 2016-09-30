using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationResource
    {
        void Delete(long id);
        Task DeleteAsync(long id);
        IResponse<Organization> Get(long id);
        Task<IResponse<Organization>> GetAsync(long id);
        IResponse<Organization> Put(OrganizationRequest request);
        Task<IResponse<Organization>> PutAsync(OrganizationRequest request);
        IResponse<Organization> Post(OrganizationRequest request);
        Task<IResponse<Organization>> PostAsync(OrganizationRequest request);
    }
}