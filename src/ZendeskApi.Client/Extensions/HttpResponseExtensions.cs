using System.Net.Http;
using System.Threading.Tasks;
using ZendeskApi.Client.Exceptions;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task IsSuccessStatusCodeOrThrowZendeskRequestException(this HttpResponseMessage response, string helpDocLink)
        {
            if (!response.IsSuccessStatusCode)
            {
                await response.ThrowZendeskRequestException(helpDocLink);
            }
        }

        public static async Task ThrowZendeskRequestException(this HttpResponseMessage response, string helpDocLink)
        {
            throw await new ZendeskRequestExceptionBuilder()
                .WithResponse(response)
                .WithHelpDocsLink(helpDocLink)
                .Build();
        }
    }
}
