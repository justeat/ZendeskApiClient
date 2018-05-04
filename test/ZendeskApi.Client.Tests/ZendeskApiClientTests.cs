using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Xunit;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskApiClientTests
    {
        private const string Authorization = "Authorization";

        private readonly ZendeskOptions _options;
        public ZendeskApiClientTests()
        {
            _options = new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                Username = "testuser@gmail.com",
                Token = "TESTTOKEN"
            };
        }

        [Fact]
        public void ShouldDefaultBaseResourceAsEndpointUri()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient();

            Assert.Equal("http://kung.fu/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUri()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient("Bruce/Li");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUriWithWeirdSlashes()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClient(options);
            var httpClient = client.CreateClient("/Bruce/Li/");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldCreateBasicAuthorizationHeader()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                Username = "testuser@gmail.com",
                Token = "TESTTOKEN"
            });
            var client = new ZendeskApiClient(options);

            var httpClient = client.CreateClient();

            var authHeader = httpClient.DefaultRequestHeaders.GetValues(Authorization).FirstOrDefault();
            var expectedHeader = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}/token:{_options.Token}"))}";
            Assert.Equal(authHeader, expectedHeader);
        }

        [Fact]
        public void ShouldCreateBearerAuthorizationHeader()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                OAuthToken = "TESTTOKEN"
            });
            var client = new ZendeskApiClient(options);

            var httpClient = client.CreateClient();

            var authHeader = httpClient.DefaultRequestHeaders.GetValues(Authorization).FirstOrDefault();
            var expectedHeader = $"Bearer {options.Value.OAuthToken}";
            Assert.Equal(authHeader, expectedHeader);
        }

    }
}
