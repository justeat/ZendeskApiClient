using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IOrganizationResource
    {
        IResponse<Organization> Get(long id);
        IResponse<Organization> Put(OrganizationRequest request);
        IResponse<Organization> Post(OrganizationRequest request);
        void Delete(long id);
    }
}