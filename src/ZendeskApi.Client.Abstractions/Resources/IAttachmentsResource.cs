using System.IO;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IAttachmentsResource
    {
        Task<Upload> UploadAsync(string fileName, Stream inputStream, string token = null);
        Task DeleteAsync(string token);
    }
}
