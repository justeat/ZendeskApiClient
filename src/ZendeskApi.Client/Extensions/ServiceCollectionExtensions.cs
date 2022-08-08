using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Options;
#pragma warning disable 618

namespace ZendeskApi.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string username,
            string token)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClient>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.Username = username;
                options.Token = token;
            });

            return services;
        }

        public static IHttpClientBuilder AddZendeskClientWithHttpClientFactory(this IServiceCollection services,
            string endpointUri,
            string username,
            string token,
            Action<HttpClient> configureClient = null)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClientFactory>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.Username = username;
                options.Token = token;
            });

            return services.AddHttpClient<ZendeskClient>("zendeskApiClient", c =>
            {
                configureClient?.Invoke(c);
            });
        }

        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string oAuthToken)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClient>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.OAuthToken = oAuthToken;
            });

            return services;
        }

        public static IServiceCollection AddZendeskClientWithHttpClientFactory(this IServiceCollection services,
            string endpointUri,
            string oAuthToken,
            Action<HttpClient> configureClient = null)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClientFactory>();

            services.AddHttpClient("zendeskApiClient", c =>
            {
                configureClient?.Invoke(c);
            });

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.OAuthToken = oAuthToken;
            });

            return services;
        }
    }
}
