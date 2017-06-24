using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;

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

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<Attachment>());
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
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/attachments#upload-files");
                }

                return (await response.Content.ReadAsAsync<Upload>());
            }
        }

        public async Task DeleteAsync(string token)
        {
            using (_loggerScope(_logger, $"DeleteAsync({token})"))
            using (var client = _apiClient.CreateClient(UploadsResourceUri))
            {
                var response = await client.DeleteAsync(token);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/attachments#delete-upload");
                }
            }
        }

    }
}
