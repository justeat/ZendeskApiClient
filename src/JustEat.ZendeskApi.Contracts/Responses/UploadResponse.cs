using System.Runtime.Serialization;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class UploadResponse : IResponse<Upload>
    {
        [DataMember(Name = "upload")]
        public Upload Item { get; set; }
    }
}