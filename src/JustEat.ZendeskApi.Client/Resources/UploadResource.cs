using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class UploadResource : ZendeskResource<Upload>, IUploadResource
    {

        protected override string ResourceUri
        {
            get { return @"/api/v2/uploads"; }
        }

        public UploadResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<Upload> Post(UploadRequest request)
        {
            var requestUrl = Client.BuildZendeskUri(ResourceUri, string.Format("filename={0}{1}", 
                request.Item.FileName,
                string.IsNullOrWhiteSpace(request.Token) ? "" : string.Format("&token={0}", request.Token)));
            var response = Post<UploadResponse>(requestUrl, request.Item);
            return response;
        }

        public new void Delete(string token)
        {
            base.Delete(token);
        }
    }
}