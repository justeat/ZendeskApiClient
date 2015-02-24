using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface IUploadResource
    {
        IResponse<Upload> Post(UploadRequest request);
        void Delete(string token);
    }
}