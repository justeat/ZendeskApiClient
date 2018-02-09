using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.Responses
{
    public class IncrementalUsersResponseTests
    {
        [Fact]
        public void ShouldDeserializeResponse()
        {
            var exampleResponse = @"
            {
              ""users"": [
                {
                  ""id"": 482198789412,
                  ""url"": ""https://kung.fu/api/v2/users/482198789412.json"",
                  ""name"": ""Test User Name"",
                  ""email"": ""test@kung.fu"",
                  ""created_at"": ""2017-10-05T08:41:59Z"",
                  ""updated_at"": ""2017-10-06T14:27:20Z"",
                  ""time_zone"": ""Copenhagen"",
                  ""phone"": ""+470123456789"",
                  ""shared_phone_number"": false,
                  ""photo"": null,
                  ""locale_id"": 34,
                  ""locale"": ""no"",
                  ""organization_id"": 145789215754,
                  ""role"": ""end-user"",
                  ""verified"": true,
                  ""external_id"": ""123456"",
                  ""tags"": [
                    ""test""
                  ],
                  ""alias"": """",
                  ""active"": true,
                  ""shared"": false,
                  ""shared_agent"": false,
                  ""last_login_at"": null,
                  ""two_factor_auth_enabled"": false,
                  ""signature"": null,
                  ""details"": """",
                  ""notes"": """",
                  ""role_type"": null,
                  ""custom_role_id"": null,
                  ""moderator"": false,
                  ""ticket_restriction"": ""requested"",
                  ""only_private_comments"": false,
                  ""restricted_agent"": true,
                  ""suspended"": false,
                  ""chat_only"": false,
                  ""default_group_id"": null,
                  ""user_fields"": {
                    ""puzzel_phone_numbers"": null
                  }
                }
              ],
              ""next_page"": ""https://kung.fu/api/v2/incremental/users?start_time=1507531370"",
              ""count"": 1,
              ""end_time"": 1507531370
            }";

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(exampleResponse));
            IncrementalUsersResponse<UserResponse> response = stream.ReadAs<IncrementalUsersResponse<UserResponse>>();

            Assert.NotNull(response.Users);
            Assert.Equal(1, response.Users.Count());
            Assert.Equal(1, response.Count);
            Assert.False(response.HasMoreResults);
            Assert.Equal("test@kung.fu", response.Users.First().Email);
        }
    }
}
