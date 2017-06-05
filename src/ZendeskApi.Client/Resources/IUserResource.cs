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
        Task<IResponse<User>> GetAsync(long id);
        Task<IListResponse<User>> GetAllAsync(List<long> ids);
        Task<IResponse<User>> PostAsync(UserRequest request);
        Task<IResponse<User>> PutAsync(UserRequest request);
    }
}