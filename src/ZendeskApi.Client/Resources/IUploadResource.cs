using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUploadResource
    {
        IResponse<Upload> Get(long id);
        IResponse<Upload> Post(UploadRequest request);
        void Delete(string token);
    }
}
