using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private static string ShowMany => "show_many";

        protected IRestClient Client;

        protected IResponse<T> Get<TResponse>(long id) where TResponse : IResponse<T>
        {
            return GetAsync<TResponse>(id).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<IResponse<T>> GetAsync<TResponse>(long id) where TResponse : IResponse<T>
        {
            var requestUri = Client.BuildUri($"{ResourceUri}/{id}");

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected IResponse<T> Get<TResponse>(string query) where TResponse : IResponse<T>
        {
            return GetAsync<TResponse>(query).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<IResponse<T>> GetAsync<TResponse>(string query) where TResponse : IResponse<T>
        {
            var requestUri = Client.BuildUri(ResourceUri, query);

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected IResponse<T> Get<TResponse>(long id, long parentId) where TResponse : IResponse<T>
        {
            return GetAsync<TResponse>(id, parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<IResponse<T>> GetAsync<TResponse>(long id, long parentId) where TResponse : IResponse<T>
        {
            var requestUri = Client.BuildUri($"{string.Format(ResourceUri, parentId)}/{id}");

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected TResponse GetAll<TResponse>(IEnumerable<long> ids) where TResponse : IListResponse<T>
        {
            return GetAllAsync<TResponse>().Result;
        }

        protected async Task<TResponse> GetAllAsync<TResponse>(IEnumerable<long> ids) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri($"{ResourceUri}/{ShowMany}", $"ids={ZendeskFormatter.ToCsv(ids)}");

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected TResponse GetAll<TResponse>(long parentId) where TResponse : IListResponse<T>
        {
            return GetAllAsync<TResponse>(parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<TResponse> GetAllAsync<TResponse>(long parentId) where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri(string.Format(ResourceUri,parentId));

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected TResponse GetAll<TResponse>() where TResponse : IListResponse<T>
        {
            return GetAllAsync<TResponse>().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<TResponse> GetAllAsync<TResponse>() where TResponse : IListResponse<T>
        {
            var requestUri = Client.BuildUri(string.Format(ResourceUri));

            return await Client.GetAsync<TResponse>(requestUri).ConfigureAwait(false);
        }

        protected IResponse<T> Put<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return PutAsync<TRequest, TResponse>(request, null).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<IResponse<T>> PutAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return await PutAsync<TRequest, TResponse>(request, null).ConfigureAwait(false);
        }

        protected IResponse<T> Put<TRequest, TResponse>(TRequest request, long? parentId)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return PutAsync<TRequest, TResponse>(request, parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<IResponse<T>> PutAsync<TRequest, TResponse>(TRequest request, long? parentId)
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri($"{resourceUri}/{request.Item.Id}");

            return await Client.PutAsync<TResponse>(requestUri, request).ConfigureAwait(false);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return PostAsync<TRequest, TResponse>(request, null).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return await PostAsync<TRequest, TResponse>(request, null).ConfigureAwait(false);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request, long? parentId) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            return PostAsync<TRequest, TResponse>(request, parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, long? parentId) 
            where TRequest : IRequest<T>
            where TResponse : IResponse<T>
        {
            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri(resourceUri);

            return await Client.PostAsync<TResponse>(requestUri, request).ConfigureAwait(false);
        }

        public void Delete(long id)
        {
            Task.WaitAll(DeleteAsync(id, null));
        }

        public async Task DeleteAsync(long id)
        {
            await DeleteAsync(id, null);
        }

        public void Delete(string token)
        {
            Task.WaitAll(DeleteAsync(token));
        }

        public async Task DeleteAsync(string token)
        {
            var requestUri = Client.BuildUri($"{ResourceUri}/{token}");

            await Client.DeleteAsync(requestUri).ConfigureAwait(false);
        }

        public void Delete(long id, long? parentId)
        {
            Task.WaitAll(DeleteAsync(id, parentId));
        }

        public async Task DeleteAsync(long id, long? parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var resourceUri = parentId == null ? ResourceUri : string.Format(ResourceUri, parentId);
            var requestUri = Client.BuildUri($"{resourceUri}/{id}");

            await Client.DeleteAsync(requestUri).ConfigureAwait(false);
        }
    }
}