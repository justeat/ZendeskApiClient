using Microsoft.Extensions.Options;
using Xunit;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskApiClientTests
    {
        [Fact]
        public void ShouldDefaultBaseResourceAsEndpointUri() {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions {
                EndpointUri = "http://kung.fu"
            });
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient();

            Assert.Equal("http://kung.fu/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUri()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu"
            });
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient("Bruce/Li");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUriWithWeirdSlashes()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu"
            });
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient("/Bruce/Li/");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }
    }
}
