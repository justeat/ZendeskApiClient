using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client
{
    public class ZendeskApiClient : IZendeskApiClient
    {
        private readonly ZendeskOptions _options;

        public ZendeskApiClient(IOptions<ZendeskOptions> options)
        {
            _options = options.Value;
        }

        public HttpClient CreateClient(string resource = null)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            }

            resource = resource?.Trim('/');

            if (!string.IsNullOrEmpty(resource))
            {
                resource = resource + "/";
            }

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{_options.EndpointUri}/{resource}"),
            };

            var authorizationHeader = Convert
                .ToBase64String(
                    Encoding.UTF8.GetBytes($"{_options.Username}/token:{_options.Token}"));

            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authorizationHeader}");

            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_options.Timeout != null)
            {
                client.Timeout = _options.Timeout.Value;
            }

            return client;
        }
    }
}
