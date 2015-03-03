using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
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

        public IHttpResponse Post(IHttpRequest request, IHttpPostedFile fileBase)
        {
            var webRequest = (HttpWebRequest)ConfigureRequest(request, "POST");

            ReadFileStreamToWebRequest(webRequest, fileBase);

            IHttpResponse response;

            try
            {
                response = BuildResponse((HttpWebResponse)webRequest.GetResponse());
            }
            catch (WebException ex)
            {
                response = HandleException(ex);
            }

            return response;
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

        private static void SetTimeout(IHttpRequest request, WebRequest webRequest)
        {
            if (request.Timeout.HasValue)
            {
                webRequest.Timeout = (int)Math.Ceiling(request.Timeout.Value.TotalMilliseconds);
            }
        }

        private void ReadFileStreamToWebRequest(WebRequest webRequest, IHttpPostedFile fileBase)
        {
            using (var requestStream = webRequest.GetRequestStream())
            {
                fileBase.InputStream.CopyTo(requestStream);

                requestStream.Close();
            }
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

        private static void AddHeaders(IHttpRequest request, HttpWebRequest webRequest)
        {
            foreach (var header in request.Headers)
            {
                switch (header.Key.ToLowerInvariant())
                {
                    case "user-agent":
                        webRequest.UserAgent = header.Value;
                        continue;
                    case "accept":
                        webRequest.Accept = header.Value;
                        continue;
                    case "content-type":
                        webRequest.ContentType = header.Value;
                        continue;
                    default:
                        webRequest.Headers.Add(header.Key, header.Value);
                        break;
                }
            }
        }

        private static void AddHeaders(IHttpResponse response, WebResponse webResponse)
        {
            response.Headers = new Dictionary<string, string>();
            foreach (var headerKey in webResponse.Headers.AllKeys)
            {
                var strings = webResponse.Headers.GetValues(headerKey);
                if (strings != null)
                    response.Headers.Add(headerKey, strings[0]);
            }
        }

        private static void ConfigureRequest(IHttpRequest request, HttpClient client)
        {
            SetTimeout(request, client);
            AddHeaders(request.Headers, client);
        }

        private static WebRequest ConfigureRequest(IHttpRequest request, string method)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(request.RequestUri);

            AddHeaders(request, webRequest);

            SetTimeout(request, webRequest);

            webRequest.Method = method;

            webRequest.ContentType = request.ContentType;

            return webRequest;
        }

        private static HttpContent BuildHttpContent(IHttpRequest request)
        {
            var stringContent = new StringContent(request.Content);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
            return stringContent;
        }

        private static IHttpResponse BuildResponse(HttpWebResponse webResponse)
        {
            var response = new HttpResponse(webResponse.StatusCode == HttpStatusCode.OK 
                || webResponse.StatusCode == HttpStatusCode.Created);

            SetResponseContent(webResponse, response);

            response.StatusCode = webResponse.StatusCode;

            AddHeaders(response, webResponse);

            return response;
        }

        private static void SetResponseContent(WebResponse webResponse, IHttpResponse response)
        {
            using (var streamResponse = webResponse.GetResponseStream())
            {
                if (streamResponse == null)
                    return;

                using (var streamRead = new StreamReader(streamResponse))
                {
                    var responseString = streamRead.ReadToEnd();
                    response.Content = responseString;
                }
            }
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