using Microsoft.Extensions.Options;
using ZendeskApi.Client.IntegrationTests.Settings;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class ZendeskClientFactory
    {
        public IZendeskClient GetClient()
        {
            var settings = new ZendeskSettings();

            return new ZendeskClient(
                new ZendeskApiClient(
                    new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
                    {
                        EndpointUri = settings.Url,
                        Username = settings.Username,
                        Token = settings.Token
                    })
                )
            );
        }
    }
}
