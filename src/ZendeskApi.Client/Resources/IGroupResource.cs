using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IGroupsResource
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<IEnumerable<Group>> GetAllAsync(long userId);
        Task<IEnumerable<Group>> GetAllAssignableAsync();
        Task<Group> GetAsync(long groupId);
        Task<Group> PostAsync(GroupRequest request);
        Task<Group> PutAsync(GroupRequest request);
        Task DeleteAsync(long groupId);
    }
}