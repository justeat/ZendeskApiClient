using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string username,
            string token
            )
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

        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string oAuthToken
        )
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClient>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.OAuthToken = oAuthToken;
            });

            return services;
        }
    }
}
