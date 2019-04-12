using System;
using System.IO;
using System.Net.Http;
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
    public class AttachmentsResource : IAttachmentsResource
    {
        private const string AttachmentsResourceUri = "api/v2/attachments";
        private const string UploadsResourceUri = "api/v2/uploads";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(AttachmentsResource).Name + ": {0}");

        public AttachmentsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<Attachment> GetAsync(long attachmentId)
        {
            using (_loggerScope(_logger, $"GetAsync({attachmentId})"))
            using (var client = _apiClient.CreateClient(AttachmentsResourceUri))
            {
                var response = await client.GetAsync(attachmentId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Attachment {0} not found", attachmentId);
                    return null;
                }

                await response.ThrowIfUnsuccessful("attachments#show-attachment");

                return await response.Content.ReadAsAsync<Attachment>();
            }
        }

        public async Task<Upload> UploadAsync(string fileName, Stream inputStream, string token = null)
        {
            using (_loggerScope(_logger, "UploadAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsBinaryAsync(
                    UploadsResourceUri + $"?filename={fileName}&token={token}",
                    inputStream,
                    fileName).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    await response.ThrowZendeskRequestException(
                        "attachments#upload-files",
                        System.Net.HttpStatusCode.Created);
                }

                var uploadResponse = await response.Content.ReadAsAsync<UploadResponse>();
                return uploadResponse.Upload;
            }
        }

        public async Task DeleteAsync(string token)
        {
            using (_loggerScope(_logger, $"DeleteAsync({token})"))
            using (var client = _apiClient.CreateClient(UploadsResourceUri))
            {
                var response = await client.DeleteAsync(token).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    await response.ThrowZendeskRequestException(
                        "attachments#delete-upload",
                        System.Net.HttpStatusCode.NoContent);
                }
            }
        }

    }
}
