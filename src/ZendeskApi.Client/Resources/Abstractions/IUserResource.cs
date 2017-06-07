using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserResource
    {
        Task DeleteAsync(long id);
        Task<User> GetAsync(long id);
        Task<IListResponse<User>> GetAllAsync(List<long> ids);
        Task<User> PostAsync(UserRequest request);
        Task<User> PutAsync(UserRequest request);
    }
}