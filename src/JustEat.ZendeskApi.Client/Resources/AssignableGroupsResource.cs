using JE.Api.ClientBase;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class AssignableGroupResource : GroupsResource, IGroupResource
    {
        private const string AssignabbleGroupUri = @"/assignable";

        public AssignableGroupResource(IBaseClient client):base(client)
        {
        }

        protected override string GetGroupResource()
        {
            return string.Format("{0}{1}", GroupsUri, AssignabbleGroupUri);
        }
    }
}
