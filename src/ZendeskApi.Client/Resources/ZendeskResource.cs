using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace ZendeskApi.Client.Resources
    {
        public abstract class ZendeskResource<T> where T : IZendeskEntity
        {
            protected IRestClient Client;

            private string _resourceName;
            protected string ResourceName
            {
                private get { return _resourceName ?? (_resourceName = GetType().Name); }
                set { _resourceName = value; }
            }

            protected IResponse<T> Get<TResponse>(string url) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return Client.Get<TResponse>(requestUri, resource: ResourceName);
            }

            protected async Task<IResponse<T>> GetAsync<TResponse>(string url) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected IResponse<T> Get<TResponse>(string url, string query) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url, query);
                return Client.Get<TResponse>(requestUri, resource: ResourceName);
            }

            protected async Task<IResponse<T>> GetAsync<TResponse>(string url, string query) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url, query);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected TResponse GetAll<TResponse>(string url, IEnumerable<long> ids) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url, $"ids={ZendeskFormatter.ToCsv(ids)}");

                return Client.Get<TResponse>(requestUri, resource: ResourceName);
            }

            protected async Task<TResponse> GetAllAsync<TResponse>(string url, IEnumerable<long> ids) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url, $"ids={ZendeskFormatter.ToCsv(ids)}");
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected TResponse GetAll<TResponse>(string url) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return Client.Get<TResponse>(requestUri, resource: ResourceName);
            }

            protected async Task<TResponse> GetAllAsync<TResponse>(string url) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected IResponse<T> Put<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");

                var requestUri = Client.BuildUri(url);
                return Client.Put<TResponse>(requestUri, request, resource: ResourceName);
            }

            protected async Task<IResponse<T>> PutAsync<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");

                var requestUri = Client.BuildUri(url);
                return await Client.PutAsync<TResponse>(requestUri, request, resource: ResourceName).ConfigureAwait(false);
            }

            protected TResponse Post<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return Client.Post<TResponse>(requestUri, request, resource: ResourceName);
            }

            protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.PostAsync<TResponse>(requestUri, request, resource: ResourceName).ConfigureAwait(false);
            }

            public void Delete(string url)
            {
                var requestUri = Client.BuildUri(url);
                Client.Delete<object>(requestUri, null, "application/json", resource: ResourceName);
            }

            public async Task DeleteAsync(string url)
            {
                var requestUri = Client.BuildUri(url);
                await Client.DeleteAsync<object>(requestUri, null, resource: ResourceName).ConfigureAwait(false);
            }

            protected void ValidateRequest<T>(IRequest<T> request) where T : IZendeskEntity
            {
                if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");
            }

            public void ValidateRequest(long id)
            {
                if (id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");
            }

        }
    }
}
