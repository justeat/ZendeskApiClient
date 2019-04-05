using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    class SingleUserResponse
    {
        [JsonProperty("user")]
        public UserResponse UserResponse { get; set; }
    }
}
