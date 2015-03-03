using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : ZendeskResource<Upload>, IUploadResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/uploads"; }
        }

        public UploadResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Upload> Get(long id)
        {
            return Get<UploadResponse>(id);
        }

        public IResponse<Upload> Post(UploadRequest request)
        {
            var requestUri = Client.BuildUri(ResourceUri, string.Format("filename={0}{1}", request.Item.FileName, request.Token ?? ""));

            return Client.PostFile<UploadResponse>(requestUri, request.Item);
        }
    }
}
