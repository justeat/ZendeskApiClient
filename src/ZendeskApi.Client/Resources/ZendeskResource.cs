using System;
using System.Net.Http;
using System.Text;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Resources
{
    public abstract class ZendeskResource<T> where T : IZendeskEntity
    {
        private readonly ZendeskOptions _options;

        public ZendeskResource(ZendeskOptions options) {
            _options = options;
        }

        protected HttpClient CreateZendeskClient(string resource)
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