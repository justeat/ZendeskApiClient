using System;
using System.Collections.Generic;
using JustEat.ZendeskApi.Client.Formatters;
using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public abstract class ZendeskResource<T>  where T : IZendeskEntity
    {
        protected abstract string ResourceUri { get; }

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

        public void Delete(long id, long? parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", resourceUri, id));

            Client.Delete(requestUri);
        }
    }
}
