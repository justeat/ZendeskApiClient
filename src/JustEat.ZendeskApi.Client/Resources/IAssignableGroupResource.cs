using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IAssignableGroupResource
    {
        ListResponse<Group> GetAll();
    }
}