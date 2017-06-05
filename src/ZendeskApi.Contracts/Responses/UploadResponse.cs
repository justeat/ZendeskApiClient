using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public class UploadResponse : IResponse<Upload>
    {
        [JsonProperty("upload")]
        public Upload Item { get; set; }
    }
}
