using System.Collections.Generic;
using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
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
