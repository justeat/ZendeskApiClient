using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using JE.Api.ClientBase.Http;
using JustEat.ZendeskApi.Client.Formatters;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using Newtonsoft.Json;

namespace JustEat.ZendeskApi.Client.Resources
{
    public abstract class ZendeskResource<T>  where T : IZendeskEntity
    {
        protected abstract string ResourceUri { get; }
        private const string UserAgentKey = "user-agent";


        protected virtual string ShowMany
        {
            get { return "show_many"; }
        }

        protected IZendeskClient Client { get; set; }

        protected IResponse<T> Get<TResponse>(long id) where TResponse : IResponse<T> 
        {
            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", ResourceUri, id));

            return Client.Get<TResponse>(requestUri);
        }

        protected IResponse<T> Get<TResponse>(string id) where TResponse : IResponse<T> 
        {
            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", ResourceUri, id));

            return Client.Get<TResponse>(requestUri);
        }

        protected TResponse GetAll<TResponse>(List<long> ids) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", ResourceUri, ShowMany), string.Format("ids={0}", ZendeskFormatter.ToCsv(ids)));

            return Client.Get<TResponse>(requestUri);
        }

        protected TResponse GetAll<TResponse>(long parentId) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildZendeskUri(string.Format(ResourceUri,parentId));

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
            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", resourceUri, request.Item.Id));

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
            var requestUri = Client.BuildZendeskUri(resourceUri);

            return Client.Post<TResponse>(requestUri, request);
        }

        public void Delete(long id)
        {
            Delete(id, null);
        }
        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", ResourceUri, id));
            Client.Delete(requestUri);
        }

        public void Delete(long id, long? parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildZendeskUri(string.Format("{0}/{1}", resourceUri, id));

            Client.Delete(requestUri);
        }


        protected TResponse Post1<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return Post<TRequest, TResponse>(request, null);
        }


        protected TResponse Post<TResponse>(Uri requestUri, HttpPostedFileBase file) where TResponse : IResponse<T>
        {
            var request = ConfigureRequest(
                new JE.Api.ClientBase.Http.HttpRequest(
                    requestUri, Client.Configuration.Headers, null, file.ContentType,
                    Client.Configuration.RequestTimeout, Client.Configuration.Proxy), "POST");

            var requestStream = request.GetRequestStream();

            file.InputStream.CopyTo(requestStream);
            requestStream.Close();

            var response = request.GetResponse();
            var result = DeserializeFromStream<TResponse>(response.GetResponseStream());

            return result;
        }

        private static WebRequest ConfigureRequest(IHttpRequest request, string method)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(request.RequestUri);

            AddHeaders(request, webRequest);

            if (!string.IsNullOrWhiteSpace(request.Proxy))
            {
                webRequest.Proxy = new WebProxy(request.Proxy);
            }

            SetTimeout(request, webRequest);

            webRequest.Method = method;

            webRequest.ContentType = request.ContentType;

            return webRequest;
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
