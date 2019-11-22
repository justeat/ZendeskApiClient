using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IAttachmentsResource
    {
        Task<Upload> UploadAsync(
            string fileName, 
            Stream inputStream, 
            string token = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            string token,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
