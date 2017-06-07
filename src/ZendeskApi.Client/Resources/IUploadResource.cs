using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IUploadResource
    {
        Task DeleteAsync(string token);
        Task<Upload> GetAsync(long id);
        Task<Upload> PostAsync(UploadRequest request);
    }
}
