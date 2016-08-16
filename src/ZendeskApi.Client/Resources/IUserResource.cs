using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserResource
    {
        void Delete(long id);
        IResponse<User> Get(long id);
        Task<IResponse<User>> GetAsync(long id);
        IListResponse<User> GetAll(List<long> ids);
        Task<IListResponse<User>> GetAllAsync(List<long> ids);
        IResponse<User> Post(UserRequest request);
        Task<IResponse<User>> PostAsync(UserRequest request);
        IResponse<User> Put(UserRequest request);
        Task<IResponse<User>> PutAsync(UserRequest request);
    }
}