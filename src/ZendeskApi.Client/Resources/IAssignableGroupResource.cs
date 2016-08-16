using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IAssignableGroupResource
    {
        ListResponse<Group> GetAll();
        Task<ListResponse<Group>> GetAllAsync();
    }
}