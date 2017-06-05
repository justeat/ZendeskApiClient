using System;
using System.Net.Http;
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

        public HttpClient CreateClient(string resource)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            }

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{_options.EndpointUri}/{resource}"),
            };

            // TODO: (ngm) add auth key?
            var authorizationHeader = Convert
                .ToBase64String(
                    Encoding.UTF8.GetBytes($"{_options.Username}/token:{_options.Token}"));

            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authorizationHeader}");

            if (_options.Timeout != null)
            {
                client.Timeout = _options.Timeout.Value;
            }

            return client;
        }
    }
}
