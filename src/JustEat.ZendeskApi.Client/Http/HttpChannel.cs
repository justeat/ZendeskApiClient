using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JustEat.ZendeskApi.Client.Http
{
    public class HttpChannel : IHttpChannel
    {
        public IHttpResponse Get(IHttpRequest request)
        {
            return GetAsync(request).Result;
        }

        public async Task<IHttpResponse> GetAsync(IHttpRequest request)
        {
            IHttpResponse response;
            using (var client = new HttpClient())
            {
                ConfigureRequest(request, client);
                try
                {
                    using (var responseMessage = await client.GetAsync(request.RequestUri).ConfigureAwait(false))
                        response = await BuildResponseAsync(responseMessage);
                }
                catch (WebException ex)
                {
                    response = HandleException(ex);
                }
                catch (TaskCanceledException)
                {
                    response = HandleTaskCanceledException(client.Timeout);
                }
            }
            return response;
        }

        public IHttpResponse Post(IHttpRequest request)
        {
            return PostAsync(request).Result;
        }

        public async Task<IHttpResponse> PostAsync(IHttpRequest request)
        {
            IHttpResponse response;
            using (var client = new HttpClient())
            {
                ConfigureRequest(request, client);
                try
                {
                    var content = BuildHttpContent(request);
                    using (var responseMessage = await client.PostAsync(request.RequestUri, content).ConfigureAwait(false))
                        response = await BuildResponseAsync(responseMessage);
                }
                catch (WebException ex)
                {
                    response = HandleException(ex);
                }
                catch (TaskCanceledException)
                {
                    response = HandleTaskCanceledException(client.Timeout);
                }
            }
            return response;
        }

        public IHttpResponse Put(IHttpRequest request)
        {
            return PutAsync(request).Result;
        }

        public async Task<IHttpResponse> PutAsync(IHttpRequest request)
        {
            IHttpResponse response;
            using (var client = new HttpClient())
            {
                ConfigureRequest(request, client);
                try
                {
                    var content = BuildHttpContent(request);
                    using (var responseMessage = await client.PutAsync(request.RequestUri, content).ConfigureAwait(false))
                        response = await BuildResponseAsync(responseMessage);
                }
                catch (WebException ex)
                {
                    response = HandleException(ex);
                }
                catch (TaskCanceledException)
                {
                    response = HandleTaskCanceledException(client.Timeout);
                }
            }
            return response;
        }

        public IHttpResponse Delete(IHttpRequest request)
        {
            return DeleteAsync(request).Result;
        }

        public async Task<IHttpResponse> DeleteAsync(IHttpRequest request)
        {
            IHttpResponse response;
            using (var client = new HttpClient())
            {
                ConfigureRequest(request, client);
                try
                {
                    using (var responseMessage = await client.DeleteAsync(request.RequestUri).ConfigureAwait(false))
                        response = await BuildResponseAsync(responseMessage);
                }
                catch (WebException ex)
                {
                    response = HandleException(ex);
                }
                catch (TaskCanceledException)
                {
                    response = HandleTaskCanceledException(client.Timeout);
                }
            }
            return response;
        }

        private static void SetTimeout(IHttpRequest request, HttpClient client)
        {
            if (!request.Timeout.HasValue)
                return;
            client.Timeout = request.Timeout.Value;
        }

        private static void AddHeaders(IEnumerable<KeyValuePair<string, string>> headers, HttpClient client)
        {
            foreach (var keyValuePair in headers)
            {
                switch (keyValuePair.Key.ToLowerInvariant())
                {
                    case "user-agent":
                        var productInfoHeaderValue = ProductInfoHeaderValue.Parse(keyValuePair.Value);
                        client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
                        continue;
                    default:
                        client.DefaultRequestHeaders.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
                        continue;
                }
            }
        }

        private static void ConfigureRequest(IHttpRequest request, HttpClient client)
        {
            SetTimeout(request, client);
            AddHeaders(request.Headers, client);
        }

        private static HttpContent BuildHttpContent(IHttpRequest request)
        {
            var stringContent = new StringContent(request.Content);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
            return stringContent;
        }

        private static async Task<IHttpResponse> BuildResponseAsync(HttpResponseMessage responseMessage)
        {
            var response = new HttpResponse(responseMessage.IsSuccessStatusCode)
            {
                Headers = GetResponseHeaders(responseMessage.Headers),
                StatusCode = responseMessage.StatusCode,
                ReasonPhrase = responseMessage.ReasonPhrase,
                Content = await responseMessage.Content.ReadAsStringAsync()
            };
            return response;
        }

        private static Dictionary<string, string> GetResponseHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders)
        {
            return responseHeaders.ToDictionary(header => header.Key, header => header.Value.First());
        }

        private static IHttpResponse HandleException(Exception e)
        {
            return new HttpResponse(false)
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = "Internal Server Error",
                Content = e.Message
            };
        }

        private static IHttpResponse HandleTaskCanceledException(TimeSpan clientTimeout)
        {
            return new HttpResponse(false)
            {
                StatusCode = HttpStatusCode.RequestTimeout,
                ReasonPhrase = "Request Time-out",
                Content = string.Format(CultureInfo.InvariantCulture, "Request took longer than {0}ms to execute", new []
                {
                    clientTimeout.Milliseconds
                })
            };
        }
    }
}