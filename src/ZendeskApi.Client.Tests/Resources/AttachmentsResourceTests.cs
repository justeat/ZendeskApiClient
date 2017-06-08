using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class AttachmentsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly AttachmentsResource _resource;

        public AttachmentsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new AttachmentsResourceSampleSite(resource));
            _resource = new AttachmentsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldCreateAttachment()
        {
            var byteArray = Encoding.UTF8.GetBytes("Hi there guys!");
            var stream = new MemoryStream(byteArray);

            var response = await _resource
                .UploadAsync("crash.log", stream, "6bk3gql82em5nmf");

            Assert.Equal("6bk3gql82em5nmf", response.Token);
            Assert.Equal("text/plain", response.Attachment.ContentType);
            Assert.Equal(new MemoryStream(byteArray).Length, response.Attachment.Size);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
