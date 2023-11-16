using Microsoft.Extensions.Options;
using ZendeskApi.Client.IntegrationTests.Settings;
using ZendeskApi.Client.Options;
#pragma warning disable 618

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class ZendeskClientFactory
    {
        public static IZendeskClient zendeskClient;
        public IZendeskClient GetClient()
        {
            zendeskClient ??= new ZendeskClient(GetApiClient());
            return zendeskClient;
        }

        public static ZendeskApiClient apiClient;
        public ZendeskApiClient GetApiClient()
        {
            var settings = new ZendeskSettings();
            apiClient ??= new ZendeskApiClient(
                    new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
                    {
                        EndpointUri = settings.Url,
                        Username = settings.Username,
                        Token = settings.Token
                    })
             );
            return apiClient;
        }
    }
}
