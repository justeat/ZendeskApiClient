using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class UserIdentityResource : ZendeskResource<UserIdentity>, IUserIdentityResource
    {
        protected override string ResourceUri {
            get { return @"/api/v2/users/{0}/identities"; }
        }

        public UserIdentityResource(IBaseClient client)
        {
            Client = client;
        }

        public IListResponse<UserIdentity> GetAll(long id)
        {
            return GetAll<UserIdentityListResponse>(id);
        }

        public IResponse<UserIdentity> Post(UserIdentityRequest request)
        {
            return Post<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }

        public IResponse<UserIdentity> Put(UserIdentityRequest request)
        {
            return Put<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }
    }
}
