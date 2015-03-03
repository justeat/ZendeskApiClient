using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class UploadResponse : IResponse<Upload>
    {
        [DataMember(Name = "upload")]
        public Upload Item { get; set; }
    }
}
