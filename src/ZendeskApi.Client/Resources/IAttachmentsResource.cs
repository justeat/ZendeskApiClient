using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Resources
{
    public interface IAttachmentsResource
    {
        Task<Upload> UploadAsync(UploadRequest request, string token = null);
        Task DeleteAsync(string token);
    }
}
