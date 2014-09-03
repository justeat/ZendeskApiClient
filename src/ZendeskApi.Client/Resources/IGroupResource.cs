using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupResource
    {
        IResponse<Group> Get(long id);
    }
}