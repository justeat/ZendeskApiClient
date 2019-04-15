using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ZendeskApi.Client.Exceptions;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task ThrowIfUnsuccessful(
            this HttpResponseMessage response, 
            string helpDocLink,
            string helpDocLinkPrefix = "support")
        {
            if (!response.IsSuccessStatusCode)
            {
                await response.ThrowZendeskRequestException(
                    helpDocLink, 
                    (HttpStatusCode?)null,
                    helpDocLinkPrefix);
            }
        }

        public static async Task ThrowZendeskRequestException(
            this HttpResponseMessage response, 
            string helpDocLink,
            HttpStatusCode? expected = null,
            string helpDocLinkPrefix = "support"
        )
        {
            await response.ThrowZendeskRequestException(
                helpDocLink,
                expected.HasValue ? new[] {expected.Value} : null,
                helpDocLinkPrefix);
        }

        public static async Task ThrowZendeskRequestException(
            this HttpResponseMessage response,
            string helpDocLink,
            HttpStatusCode[] expected = null,
            string helpDocLinkPrefix = "support"
        )
        {
            var builder = new ZendeskRequestExceptionBuilder()
                .WithResponse(response)
                .WithHelpDocsLink($"/{helpDocLinkPrefix}/{helpDocLink}");

            if (expected != null)
                builder.WithExpectedHttpStatus(expected);

            throw await builder.Build();
        }
    }
}
