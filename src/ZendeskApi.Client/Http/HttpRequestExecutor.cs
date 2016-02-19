using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi.Client.Serialization;

namespace ZendeskApi.Client.Http
{
    public class HttpRequestExecutor
    {
        private string _contentType;
        private readonly HttpMethod _method;
        private Uri _uri;
        private object _item;
        private ISerializer _serializer;
        private DefaultConfiguration _configuration;
        private IHttpChannel _http;
        private IHttpExceptionHandler _exceptionHandler;
        private IInvocationTimer _timer;
        private string _resource;
        private string _operation;
        private string _clientName;

        public HttpRequestExecutor(HttpMethod method)
        {
            _method = method;
        }

        public HttpRequestExecutor WithConfiguration(DefaultConfiguration configuration)
        {
            _configuration = configuration;
            return this;
        }

        public HttpRequestExecutor WithUri(Uri uri)
        {
            _uri = uri;
            return this;
        }

        public HttpRequestExecutor WithPayload(object item)
        {
            _item = item;
            return this;
        }

        public HttpRequestExecutor WithContentType(string contentType)
        {
            _contentType = contentType;
            return this;
        }

        public HttpRequestExecutor WithSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            return this;
        }

        public HttpRequestExecutor WithHttpChannel(IHttpChannel http)
        {
            _http = http;
            return this;
        }

        public HttpRequestExecutor WithExceptionHandler(IHttpExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            return this;
        }

        public HttpRequestExecutor WithInvocationTimer(IInvocationTimer timer)
        {
            _timer = timer;
            return this;
        }

        public HttpRequestExecutor WithResource(string resource)
        {
            _resource = resource;
            return this;
        }

        public HttpRequestExecutor WithOperation(string operation)
        {
            _operation = operation;
            return this;
        }

        public HttpRequestExecutor WithClient(string clientName)
        {
            _clientName = clientName;
            return this;
        }

        public Response<T> Execute<T>()
        {
            return ExecuteWithTimer(() =>
            {
                var request = BuildRequest();
                var response = _http.Execute(request);
                return PackageResponse<T>(response);
            }, _timer.RegisterElapsedTime);
        }

        public async Task<T> ExecuteAsync<T>()
        {
            return await ExecuteWithTimer(async () =>
            {
                var request = BuildRequest();
                var response = await _http.ExecuteAsync(request);
                return PackageResponse<T>(response);
            }, _timer.RegisterElapsedTimeAsync);
        }

        private TResponse ExecuteWithTimer<TResponse>(Func<TResponse> getResponse, Func<OperationEvent, Func<TResponse>, TResponse> logTiming)
        {
            if (new[] { _resource, _operation, _clientName }.Any(string.IsNullOrEmpty)) return getResponse();

            var operationEvent = new OperationEvent(_configuration.Feature, _clientName, _resource, _operation);
            return logTiming(operationEvent, getResponse);
        }

        private IHttpRequest BuildRequest()
        {
            var content = _item != null ? _serializer.Serialize(_item) : null;
            var requestTimeout = _configuration != null ? _configuration.RequestTimeout : null;
            var proxy = _configuration != null ? _configuration.Proxy : null;
            var keepAlive = _configuration != null && _configuration.KeepAlive;

            var request = new HttpRequest(_method, _uri, Headers, content, _contentType, requestTimeout, proxy, keepAlive);

            return request;
        }

        private IEnumerable<KeyValuePair<string, string>> Headers
        {
            get
            {
                return _configuration != null
                    // Create copy for thread safety
                    ? _configuration.Headers.Select(h => new KeyValuePair<string, string>(h.Key, h.Value)).ToList()
                    : Enumerable.Empty<KeyValuePair<string, string>>();
            }
        }

        private Response<T> PackageResponse<T>(IHttpResponse response)
        {
            ValidateResponse(response);
            var content = _serializer.Deserialize<T>(response.Content);
            return new Response<T>(content, response);
        }

        private void ValidateResponse(IHttpResponse response)
        {
            if (!response.IsSuccessStatusCode)
            {
                _exceptionHandler.HandleError(response.StatusCode, response.Content);
            }
        }
    }
}
