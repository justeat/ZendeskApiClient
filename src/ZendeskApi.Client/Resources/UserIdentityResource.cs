using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : ZendeskResource<UserIdentity>, IUserIdentityResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/users/{0}/identities"; }
        }

        public UserIdentityResource(IRestClient client)
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
