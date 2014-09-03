using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserResource : ZendeskResource<User>, IUserResource
    {
        protected override string ResourceUri {
            get { return @"/api/v2/users/"; }
        }

        public UserResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<User> Get(long id)
        {
            return Get<UserResponse>(id);
        }

        public IListResponse<User> GetAll(List<long> ids)
        {
            return GetAll<UserListResponse>(ids);
        }

        public IResponse<User> Post(UserRequest request)
        {
            return Post<UserRequest, UserResponse>(request);
        }

        public IResponse<User> Put(UserRequest request)
        {
            return Put<UserRequest, UserResponse>(request);
        }
    }
}
