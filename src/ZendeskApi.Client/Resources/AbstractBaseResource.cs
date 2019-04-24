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

        protected async Task<T> GetAsync<T>(
            string resource,
            string docs,
            string scope,
            PagerParameters pager = null,
            JsonConverter converter = null)
            where T : class
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.GetAsync(resource, pager)
                    .ConfigureAwait(false);

                await response
                    .ThrowIfUnsuccessful($"{DocsResource}#{docs}");

                if (converter == null)
                {
                    return await response
                        .Content
                        .ReadAsAsync<T>();
                }

                return await response
                    .Content
                    .ReadAsAsync<T>(converter);
            }
        }

        protected async Task<T> GetWithNotFoundCheckAsync<T>(
            string resource,
            string docs,
            string scope,
            string notFoundLogMessage,
            PagerParameters pager = null)
            where T : class
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.GetAsync(resource, pager)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Logger.LogInformation(notFoundLogMessage);
                    return null;
                }

                await response
                    .ThrowIfUnsuccessful($"{DocsResource}#{docs}");

                return await response
                    .Content
                    .ReadAsAsync<T>();
            }
        }

        protected async Task<TResponse> CreateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created,
            string scope = "CreateAsync")
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(resource, item)
                    .ConfigureAwait(false);

                if (response.StatusCode != expectedStatusCode)
                {
                    await response.ThrowZendeskRequestException(
                        $"{DocsResource}#{docs}",
                        expectedStatusCode);
                }

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task<HttpResponseMessage> UpdateAsync(
            string resource,
            string docs,
            string scope = "UpdateAsync")
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync(resource, new StringContent(string.Empty))
                    .ConfigureAwait(false);

                await response.ThrowIfUnsuccessful($"{DocsResource}#{docs}");

                return response;
            }
        }

        public async Task UpdateAsync(
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

            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync($"{resource}?ids={idsAsCsv}", new StringContent(string.Empty))
                    .ConfigureAwait(false);

                await response.ThrowIfUnsuccessful(
                    $"{DocsResource}#{docs}",
                    HttpStatusCode.OK);
            }
        }

        public async Task<TResponse> UpdateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string scope = "UpdateAsync")
            where TResponse : class
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync(resource, item)
                    .ConfigureAwait(false);

                await response.ThrowIfUnsuccessful($"{DocsResource}#{docs}");

                return await response
                    .Content
                    .ReadAsAsync<TResponse>();
            }
        }

        public async Task<TResponse> UpdateWithNotFoundCheckAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string notFoundLogMessage,
            string scope = "UpdateAsync")
            where TResponse : class
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync(resource, item)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Logger.LogInformation(notFoundLogMessage);
                    return null;
                }

                await response.ThrowIfUnsuccessful($"{DocsResource}#{docs}");

                return await response
                    .Content
                    .ReadAsAsync<TResponse>();
            }
        }

        public async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
            where TResponse : class
        {
            var response = await DeleteAsync(
                resource,
                id,
                docs,
                expectedHttpStatusCode,
                scope);

            return await response
                .Content
                .ReadAsAsync<TResponse>();
        }


        public async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
        {
            if (scope == null)
                scope = $"DeleteAsync{id}";

            return await DeleteAsync(
                $"{resource}/{id}",
                docs,
                expectedHttpStatusCode,
                scope);
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null)
        {
            if (scope == null)
                scope = $"DeleteAsync";

            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.DeleteAsync(resource)
                    .ConfigureAwait(false);

                if (response.StatusCode != expectedHttpStatusCode)
                {
                    await response.ThrowZendeskRequestException(
                        $"{DocsResource}#{docs}",
                        expectedHttpStatusCode);
                }

                return response;
            }
        }

        public async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = null)
            where TResponse : class
        {
            var response = await DeleteAsync(
                resource,
                ids,
                docs,
                scope);

            return await response
                .Content
                .ReadAsAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> DeleteAsync(
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

            if (scope == null)
                scope = $"DeleteAsync({idsAsCsv})";

            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                var response = await client.DeleteAsync($"{resource}?ids={idsAsCsv}")
                    .ConfigureAwait(false);

                await response.ThrowIfUnsuccessful(
                    $"{DocsResource}#{docs}",
                    HttpStatusCode.OK);

                return response;
            }
        }
    }
}