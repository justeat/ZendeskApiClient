using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client
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

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.Username = username;
                options.Token = token;
            });

            return services;
        }
    }
}
