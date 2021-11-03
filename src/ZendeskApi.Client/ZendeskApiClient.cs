using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client
{
    [Obsolete("Use ZendeskApiClientFactory as this uses IHttpClientFactory")]
    public class ZendeskApiClient : IZendeskApiClient
    {
        private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;
        private readonly ZendeskOptions _options;

        public ZendeskApiClient(IOptions<ZendeskOptions> options, Func<HttpMessageHandler> httpMessageHandlerFactory)
        : this(options)
        {
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
        }

        public ZendeskApiClient(IOptions<ZendeskOptions> options)
        {
            _options = options.Value;
            _httpMessageHandlerFactory = () =>
            {
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
                }
                return handler;
            };
        }

        public HttpClient CreateClient(string resource = null)
        {
            resource = resource?.Trim('/');

            if (!string.IsNullOrEmpty(resource))
            {
                resource = resource + "/";
            }

            if(_options.EndpointUri != null || !string.IsNullOrEmpty(_options.EndpointUri)){
                Console.WriteLine("_options.EndpointUri "+ "all good");
            }
            Console.WriteLine("_options.EndpointUri "+ _options.EndpointUri);
            Console.WriteLine("ENV VAR " + Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Url"));
            var handler = _httpMessageHandlerFactory();

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{_options.EndpointUri}/{resource}"),
            };

            var authorizationHeader = _options.GetAuthorizationHeader();

            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_options.Timeout != null)
            {
                client.Timeout = _options.Timeout.Value;
            }

            return client;
        }

        public HttpClient CreateServiceStatusClient()
        {
            var handler = _httpMessageHandlerFactory();

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://status.zendesk.com")
            };

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
