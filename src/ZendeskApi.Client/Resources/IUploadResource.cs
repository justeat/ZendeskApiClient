using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUploadResource
    {
        Task DeleteAsync(string token);
        Task<IResponse<Upload>> GetAsync(long id);
        Task<IResponse<Upload>> PostAsync(UploadRequest request);
    }
}
