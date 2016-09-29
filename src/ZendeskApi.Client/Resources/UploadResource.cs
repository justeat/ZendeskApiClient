using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : IUploadResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = "/api/v2/uploads";

        public UploadResource(IRestClient client)
        {
            _client = client;
        }

        public void Delete(string token)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{token}");
            _client.Delete(requestUri);
        }

        public IResponse<Upload> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<UploadResponse>(requestUri);
        }

        public async Task<IResponse<Upload>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<UploadResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<Upload> Post(UploadRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri, $"filename={request.Item.FileName}{request.Token ?? ""}");
            return _client.PostFile<UploadResponse>(requestUri, request.Item);
        }
    }
}
