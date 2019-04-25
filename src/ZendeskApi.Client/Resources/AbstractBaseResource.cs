using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public abstract class AbstractBaseResource<TResource>
    {
        protected readonly IZendeskApiClient ApiClient;
        protected readonly ILogger Logger;
        protected readonly string DocsResource;

        protected readonly Func<ILogger, string, IDisposable> LoggerScope = LoggerMessage.DefineScope<string>(typeof(TResource).Name + ": {0}");

        protected AbstractBaseResource(
            IZendeskApiClient apiClient, 
            ILogger logger,
            string docsResource)
        {
            ApiClient = apiClient;
            Logger = logger;
            DocsResource = docsResource;
        }

        protected async Task<HttpResponseMessage> ExecuteRequest(
            Func<HttpClient, Task<HttpResponseMessage>> requestBodyAccessor,
            string scope)
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                return await requestBodyAccessor(client);
            }
        }

        protected async Task<T> GetAsync<T>(
            string resource,
            string docs,
            string scope,
            PagerParameters pager = null,
            JsonConverter converter = null)
            where T : class
        {
            return await ExecuteRequest(async client => 
                    await client.GetAsync(resource, pager).ConfigureAwait(false),
                    scope)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>(converter);
        }

        protected async Task<T> GetWithNotFoundCheckAsync<T>(
            string resource,
            string docs,
            string scope,
            string notFoundLogMessage,
            PagerParameters pager = null)
            where T : class
        {
            return await ExecuteRequest(async client =>
                    await client.GetAsync(resource, pager).ConfigureAwait(false), 
                    scope)
                .IsNullWhen(HttpStatusCode.NotFound)
                .LogInformationWhenNull(Logger, notFoundLogMessage)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>();
        }

        protected async Task<HttpResponseMessage> CreateAsync<TRequest>(
            string resource,
            TRequest item,
            string docs,
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created,
            string scope = "CreateAsync")
        {
            return await ExecuteRequest(async client => 
                    await client.PostAsJsonAsync(resource, item).ConfigureAwait(false), 
                    scope)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}", expectedStatusCode);
        }

        protected async Task<TResponse> CreateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created,
            string scope = "CreateAsync")
            where TResponse : class
        {
            return await CreateAsync<TRequest>(
                    resource,
                    item,
                    docs,
                    expectedStatusCode,
                    scope)
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> UpdateAsync(
            string resource,
            string docs,
            string scope = "UpdateAsync")
        {
            return await ExecuteRequest(async client => 
                    await client.PutAsJsonAsync(resource, new StringContent(string.Empty)).ConfigureAwait(false), 
                    scope)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}");
        }

        protected async Task<HttpResponseMessage> UpdateAsync(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = "UpdateAsync")
        {
            if (ids == null)
                throw new ArgumentNullException($"{nameof(ids)} must not be null", nameof(ids));

            if (ids.Count == 0 || ids.Count > 100)
                throw new ArgumentException($"{nameof(ids)} must have [0..100] elements", nameof(ids));

            var idsAsCsv = ZendeskFormatter.ToCsv(ids);

            return await UpdateAsync(
                $"{resource}?ids={idsAsCsv}",
                docs,
                scope);
        }

        protected async Task<TResponse> UpdateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string scope = "UpdateAsync")
            where TResponse : class
        {
            return await ExecuteRequest(async client => 
                    await client.PutAsJsonAsync(resource, item).ConfigureAwait(false), 
                    scope)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<TResponse> UpdateWithNotFoundCheckAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string notFoundLogMessage,
            string scope = "UpdateAsync")
            where TResponse : class
        {
            return await ExecuteRequest(async client => 
                    await client.PutAsJsonAsync(resource, item).ConfigureAwait(false), 
                    scope)
                .IsNullWhen(HttpStatusCode.NotFound)
                .LogInformationWhenNull(Logger, notFoundLogMessage)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
        {
            return await ExecuteRequest(async client => 
                    await client.DeleteAsync(resource).ConfigureAwait(false), 
                    scope ?? "DeleteAsync")
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}", expectedHttpStatusCode);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
        {
            return await DeleteAsync(
                $"{resource}/{id}",
                docs,
                expectedHttpStatusCode,
                scope ?? $"DeleteAsync{id}");
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
            where TResponse : class
        {
            return await DeleteAsync(
                resource,
                id,
                docs,
                expectedHttpStatusCode,
                scope)
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = null)
        {
            if (ids == null)
                throw new ArgumentNullException($"{nameof(ids)} must not be null", nameof(ids));

            if (ids.Count == 0 || ids.Count > 100)
                throw new ArgumentException($"{nameof(ids)} must have [0..100] elements", nameof(ids));

            var idsAsCsv = ZendeskFormatter.ToCsv(ids);

            return await DeleteAsync(
                $"{resource}?ids={idsAsCsv}",
                docs,
                HttpStatusCode.OK,
                scope ?? $"DeleteAsync({idsAsCsv})");
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = null)
            where TResponse : class
        {
            return await DeleteAsync(
                    resource,
                    ids,
                    docs,
                    scope)
                .ReadContentAsAsync<TResponse>();
        }
    }
}