using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUploadResource
    {
        void Delete(string token);
        IResponse<Upload> Get(long id);
        Task<IResponse<Upload>> GetAsync(long id);
        IResponse<Upload> Post(UploadRequest request);
    }
}
