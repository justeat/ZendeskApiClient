using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Logging;
using ZendeskApi.Client.Serialization;

namespace ZendeskApi.Client.ClientBase
{
    public abstract class BaseClientImplementation
    {
        private const string DefaultContentType = "application/json";
        private readonly BaseClientDependencies _baseClientDependencies;
        private string _clientName;

        protected string ClientName
        {
            private get { return _clientName ?? (_clientName = GetType().Name); }
            set { _clientName = value; }
        }

        public Uri BaseUri { get; private set; }

        private IHttpExceptionHandler HttpExceptionHandler
        {
            get { return _baseClientDependencies.HttpExceptionHandler; }
        }

        public IInvocationTimer InvocationTimer
        {
            get { return _baseClientDependencies.InvocationTimer; }
        }

        public DefaultConfiguration Configuration
        {
            get { return _baseClientDependencies.Configuration; }
        }

        private IHttpChannel InnerChannel
        {
            get { return _baseClientDependencies.InnerChannel; }
        }

        public IHttpChannel Http
        {
            get { return _baseClientDependencies.Http; }
        }

        public ISerializer Serializer
        {
            get { return _baseClientDependencies.Serializer; }
        }

        public ILogAdapter Logger
        {
            get { return _baseClientDependencies.Logger; }
        }

        public EnabledInterceptors Interceptors
        {
            get { return _baseClientDependencies.Interceptors; }
        }

        public Uri BuildUri(string handler, string query = "")
        {
            var uriBuilder = new UriBuilder(BaseUri)
            {
                Path = handler,
                Query = query
            };

            return uriBuilder.Uri;
        }

        protected BaseClientImplementation(Uri baseUri, DefaultConfiguration configuration, IOptionalDependencies dependencies)
        {
            if (baseUri == null) throw new ArgumentNullException("baseUri");
            _baseClientDependencies = new BaseClientDependencies(configuration, dependencies);
            BaseUri = baseUri;

            Logger.Debug(
                string.Format(
                    "Created API client. BaseUri: {0}, Serializer: {1}, HttpChannel: {2}, Logger: {3}",
                    BaseUri, Serializer.GetType().Name, InnerChannel.GetType().Name, Logger.GetType().Name));
        }

        #region IBaseClient Implementations

        public T Get<T>(Uri requestUri, string resource, string operation)
        {
            return GetExecutor(requestUri, resource, operation).Execute<T>();
        }

        public Task<T> GetAsync<T>(Uri requestUri, string resource, string operation)
        {
            return GetExecutor(requestUri, resource, operation).ExecuteAsync<T>();
        }

        public T Post<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PostExecutor(requestUri, item, contentType, resource, operation).Execute<T>();
        }

        public Task<T> PostAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PostExecutor(requestUri, item, contentType, resource, operation).ExecuteAsync<T>();
        }

        public T Put<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PutExecutor(requestUri, item, contentType, resource, operation).Execute<T>();
        }

        public Task<T> PutAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PutExecutor(requestUri, item, contentType, resource, operation).ExecuteAsync<T>();
        }

        public T Patch<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PatchExecutor(requestUri, item, contentType, resource, operation).Execute<T>();
        }

        public Task<T> PatchAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return PatchExecutor(requestUri, item, contentType, resource, operation).ExecuteAsync<T>();
        }

        public void Delete(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            Delete<object>(requestUri, item, contentType, resource, operation);
        }

        public async Task DeleteAsync(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            await DeleteAsync<object>(requestUri, item, contentType, resource, operation);
        }

        public T Delete<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return DeleteExecutor(requestUri, item, contentType, resource, operation).Execute<T>();
        }

        public Task<T> DeleteAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            return DeleteExecutor(requestUri, item, contentType, resource, operation).ExecuteAsync<T>();
        }

        #endregion

        private HttpRequestExecutor GetExecutor(Uri uri, string resource, string operation)
        {
            return ConstructExecutor(HttpMethod.Get).WithResource(resource).WithOperation(operation).WithUri(uri);
        }

        private HttpRequestExecutor PostExecutor(Uri uri, object payload, string contentType, string resource, string operation)
        {
            return PopulateExecutor(ConstructExecutor(HttpMethod.Post).WithUri(uri), payload, contentType, resource, operation);
        }

        private HttpRequestExecutor PutExecutor(Uri uri, object payload, string contentType, string resource, string operation)
        {
            return PopulateExecutor(ConstructExecutor(HttpMethod.Put).WithUri(uri), payload, contentType, resource, operation);
        }

        private HttpRequestExecutor PatchExecutor(Uri uri, object payload, string contentType, string resource, string operation)
        {
            return PopulateExecutor(ConstructExecutor(new HttpMethod("PATCH")).WithUri(uri), payload, contentType, resource, operation);
        }

        private HttpRequestExecutor DeleteExecutor(Uri uri, object payload, string contentType, string resource, string operation)
        {
            return PopulateExecutor(ConstructExecutor(HttpMethod.Delete).WithUri(uri), payload, contentType, resource, operation);
        }

        private HttpRequestExecutor PopulateExecutor(HttpRequestExecutor executor, object payload, string contentType, string resource, string operation)
        {
            if (string.IsNullOrEmpty(contentType)) contentType = DefaultContentType;
            return executor
                .WithResource(resource)
                .WithOperation(operation)
                .WithPayload(payload)
                .WithContentType(contentType);
        }

        private HttpRequestExecutor ConstructExecutor(HttpMethod method)
        {
            return new HttpRequestExecutor(method)
                .WithConfiguration(Configuration)
                .WithSerializer(Serializer)
                .WithHttpChannel(Http)
                .WithExceptionHandler(HttpExceptionHandler)
                .WithInvocationTimer(InvocationTimer)
                .WithClient(ClientName);
        }
    }
}
