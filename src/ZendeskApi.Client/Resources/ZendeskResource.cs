using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public abstract class ZendeskResource<T>  where T : IZendeskEntity
    {
        protected abstract string ResourceUri { get; }
        private const string UserAgentKey = "user-agent";

        private string ShowMany
        {
            get { return "show_many"; }
        }

        protected IRestClient Client;

        protected IResponse<T> Get<TResponse>(long id) where TResponse : IResponse<T> 
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, id));

            return Client.Get<TResponse>(requestUri);
        }

        protected IResponse<T> Get<TResponse>(string query) where TResponse : IResponse<T>
        {
            var requestUri = Client.BuildUri(ResourceUri, query);

            return Client.Get<TResponse>(requestUri);
        }

        protected TResponse GetAll<TResponse>(IEnumerable<long> ids) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, ShowMany), string.Format("ids={0}", ZendeskFormatter.ToCsv(ids)));

            return Client.Get<TResponse>(requestUri);
        }

        protected TResponse GetAll<TResponse>(long parentId) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri(string.Format(ResourceUri,parentId));

            return Client.Get<TResponse>(requestUri);
        }

        protected IResponse<T> Put<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return Put<TRequest, TResponse>(request, null);
        }

        protected IResponse<T> Put<TRequest, TResponse>(TRequest request, long? parentId)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", resourceUri, request.Item.Id));

            return Client.Put<TResponse>(requestUri, request);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return Post<TRequest, TResponse>(request, null);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request, long? parentId) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri(resourceUri);

            return Client.Post<TResponse>(requestUri, request);
        }

        public void Delete(long id)
        {
            Delete(id, null);
        }

        public void Delete(string token)
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, token));

            Client.Delete(requestUri);
        }

        public void Delete(long id, long? parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", resourceUri, id));

            Client.Delete(requestUri);
        }

        private static void AddHeaders(IHttpRequest request, HttpWebRequest webRequest)
        {
            foreach (var header in request.Headers)
            {
                switch (header.Key.ToLowerInvariant())
                {
                    case UserAgentKey:
                        webRequest.UserAgent = header.Value;
                        break;
                    default:
                        webRequest.Headers.Add(header.Key, header.Value);
                        break;
                }
            }
        }

        private static void SetTimeout(IHttpRequest request, WebRequest webRequest)
        {
            if (request.Timeout.HasValue)
            {
                webRequest.Timeout = (int)Math.Ceiling(request.Timeout.Value.TotalMilliseconds);
            }
        }

        private static TResponse DeserializeFromStream<TResponse>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<TResponse>(sr.ReadToEnd());
            }
        }
    }
}
