using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class LocaleResource : AbstractBaseResource<LocaleResource>,
        ILocaleResource
    {
        private const string ResourceUri = "api/v2/locales";

        public LocaleResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "locales")
        { }

        public async Task<IReadOnlyList<Locale>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(ResourceUri, token).ConfigureAwait(false),
                    "GetAllAsync",
                    cancellationToken)
                .ThrowIfUnsuccessful("list-locales")
                .ReadContentAsAsync<LocaleListResponse>();

            return response.Locales;
        }

        public async Task<IReadOnlyList<Locale>> GetAllPublicAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"{ResourceUri}/public", token).ConfigureAwait(false),
                    "GetAllPublicAsync",
                    cancellationToken)
                .ThrowIfUnsuccessful("list-available-public-locales")
                .ReadContentAsAsync<LocaleListResponse>();

            return response.Locales;
        }

        public async Task<IReadOnlyList<Locale>> GetAllAgentAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await ExecuteRequest(async (client, token) =>
                        await client.GetAsync($"{ResourceUri}/agent", token).ConfigureAwait(false),
                    "GetAllAgentAsync",
                    cancellationToken)
                .ThrowIfUnsuccessful("list-locales-for-agent")
                .ReadContentAsAsync<LocaleListResponse>();

            return response.Locales;
        }

        public async Task<Locale> GetAsync(
            long id, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<LocaleResponse>(
                $"{ResourceUri}/{id}",
                "show-locale",
                $"GetAsync({id})",
                $"LocaleResponse {id} not found",
                cancellationToken: cancellationToken);

            return response?.Locale;
        }

        public async Task<Locale> GetCurrentAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<LocaleResponse>(
                $"{ResourceUri}/current",
                "show-current-locale",
                $"GetCurrent",
                $"LocaleResponse not found",
                cancellationToken: cancellationToken);

            return response?.Locale;
        }

        public async Task<Locale> GetBestLanguageForUserAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<LocaleResponse>(
                $"{ResourceUri}/detect_best_locale",
                "detect-best-language-for-user",
                $"GetCurrent",
                $"LocaleResponse not found",
                cancellationToken: cancellationToken);

            return response?.Locale;
        }
    }
}