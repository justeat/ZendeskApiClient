using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<IPagination<Group>> GetAllAsync();
        Task<IPagination<Group>> GetAllAsync(long userId);
        Task<IPagination<Group>> GetAllAssignableAsync();
        Task<Group> GetAsync(long groupId);
        Task<Group> PostAsync(Group group);
        Task<Group> PutAsync(Group group);
        Task DeleteAsync(long groupId);
    }
}