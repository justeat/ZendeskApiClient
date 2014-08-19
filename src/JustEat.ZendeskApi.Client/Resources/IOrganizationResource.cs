using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IOrganizationResource
    {
        IResponse<Organization> Get(long id);
        IResponse<Organization> Put(OrganizationRequest request);
        IResponse<Organization> Post(OrganizationRequest request);
        void Delete(long id);
    }
}