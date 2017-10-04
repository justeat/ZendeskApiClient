using System.IO;
using System.Text;
using Xunit;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.Responses
{
    public class UploadResponseTests
    {
        [Fact]
        public void ShouldDeserializeResponse()
        {
            var exampleResponse = @"
            {
              ""upload"": {
                ""token"": ""6jsaDLkjdf3r087uJHFf83"",
                ""expires_at"": ""2017-09-25T08:47:22Z"",
                ""attachments"": [
                  {
                    ""url"": ""https://kung.fu/api/v2/attachments/123737562314.json"",
                    ""id"": 123737562314,
                    ""file_name"": ""crash.log"",
                    ""content_url"": ""https://kung.fu.com/attachments/token/6jsaDLkjdf3r087uJHFf83/?name=crash.log"",
                    ""mapped_content_url"": ""https://kung.fu.com/attachments/token/6jsaDLkjdf3r087uJHFf83/?name=crash.log"",
                    ""content_type"": ""text/plain"",
                    ""size"": 10,
                    ""width"": null,
                    ""height"": null,
                    ""inline"": false,
                    ""thumbnails"": []
                  }
                ],
                ""attachment"": {
                  ""url"": ""https://kung.fu.com/api/v2/attachments/123737562314.json"",
                  ""id"": 123737562314,
                  ""file_name"": ""crash.log"",
                  ""content_url"": ""https://kung.fu.com/attachments/token/6jsaDLkjdf3r087uJHFf83/?name=crash.log"",
                  ""mapped_content_url"": ""https://kung.fu.com/attachments/token/6jsaDLkjdf3r087uJHFf83/?name=crash.log"",
                  ""content_type"": ""text/plain"",
                  ""size"": 10,
                  ""width"": null,
                  ""height"": null,
                  ""inline"": false,
                  ""thumbnails"": []
                }
              }
            }";

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(exampleResponse));
            UploadResponse response = stream.ReadAs<UploadResponse>();

            Assert.NotNull(response.Upload);
            Assert.NotNull(response.Upload.Attachment);
            Assert.Equal("6jsaDLkjdf3r087uJHFf83", response.Upload.Token);
            Assert.Equal(123737562314L, response.Upload.Attachment.Id);
        }
    }
}
