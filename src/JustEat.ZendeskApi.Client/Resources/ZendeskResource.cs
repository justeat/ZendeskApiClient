using System;
using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Formatters;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public abstract class ZendeskResource<T>  where T : IZendeskEntity
    {
        protected abstract string ResourceUri { get; }

        protected virtual string ShowMany
        {
            get { return "show_many"; }
        }

        protected IBaseClient Client;

        protected IResponse<T> Get<TResponse>(long id) where TResponse : IResponse<T> 
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, id));

            return Client.Get<TResponse>(requestUri);
        }

        protected TResponse GetAll<TResponse>(List<long> ids) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, ShowMany), string.Format("ids={0}", ZendeskFormatter.ToCsv(ids)));

            return Client.Get<TResponse>(requestUri);
        }

        protected IResponse<T> Put<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, request.Item.Id));

            return Client.Put<TResponse>(requestUri, request);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            var requestUri = Client.BuildUri(ResourceUri);

            return Client.Post<TResponse>(requestUri, request);
        }

        public void Delete(long id)
        {
            var requestUri = Client.BuildUri(string.Format("{0}/{1}", ResourceUri, id));

            Client.Delete(requestUri);
        }
    }
}
