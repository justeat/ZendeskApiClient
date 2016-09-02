using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : ZendeskResource<Upload>, IUploadResource
    {
        protected override string ResourceUri => @"/api/v2/uploads";

        public UploadResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Upload> Get(long id)
        {
            return Get<UploadResponse>(id);
        }

        public async Task<IResponse<Upload>> GetAsync(long id)
        {
            return await GetAsync<UploadResponse>(id).ConfigureAwait(false);
        }

        public IResponse<Upload> Post(UploadRequest request)
        {
            var requestUri = Client.BuildUri(ResourceUri, $"filename={request.Item.FileName}{request.Token ?? ""}");

            return Client.PostFile<UploadResponse>(requestUri, request.Item);
        }
    }
}
