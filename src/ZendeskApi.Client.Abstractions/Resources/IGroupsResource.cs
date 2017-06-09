using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<IEnumerable<Group>> GetAllAsync(long userId);
        Task<IEnumerable<Group>> GetAllAssignableAsync();
        Task<Group> GetAsync(long groupId);
        Task<Group> PostAsync(Group group);
        Task<Group> PutAsync(Group group);
        Task DeleteAsync(long groupId);
    }
}