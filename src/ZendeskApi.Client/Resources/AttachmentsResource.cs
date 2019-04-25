using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/attachments"/>
    /// </summary>
    public class AttachmentsResource : AbstractBaseResource<AttachmentsResource>, 
        IAttachmentsResource
    {
        private const string AttachmentsResourceUri = "api/v2/attachments";
        private const string UploadsResourceUri = "api/v2/uploads";

        public AttachmentsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "attachments")
        { }

        public async Task<Attachment> GetAsync(long attachmentId)
        {
            return await GetWithNotFoundCheckAsync<Attachment>(
                $"{AttachmentsResourceUri}/{attachmentId}",
                "show-attachment",
                $"GetAsync({attachmentId})",
                $"Attachment {attachmentId} not found");
        }

        public async Task<Upload> UploadAsync(string fileName, Stream inputStream, string token = null)
        {
            var attachmentResponse = await ExecuteRequest(async client => 
                    await client.PostAsBinaryAsync(
                        UploadsResourceUri + $"?filename={fileName}&token={token}",
                        inputStream,
                        fileName)
                    .ConfigureAwait(false), 
                    "UploadAsync")
                .ThrowIfUnsuccessful("attachments#upload-files", HttpStatusCode.Created)
                .ReadContentAsAsync<UploadResponse>();

            return attachmentResponse.Upload;
        }

        public async Task DeleteAsync(string token)
        {
            await DeleteAsync(
                $"{UploadsResourceUri}/{token}",
                "permanently-delete-user");
        }
    }
}
