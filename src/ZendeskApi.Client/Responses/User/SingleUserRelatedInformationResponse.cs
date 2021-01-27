using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    class SingleUserRelatedInformationResponse
    {
        [JsonProperty("user_related")]
        public UserRelatedInformationResponse UserRelatedInformationResponse { get; set; }
    }
}