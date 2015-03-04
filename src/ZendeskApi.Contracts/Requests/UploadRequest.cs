using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public class UploadRequest 
    {
        public IHttpPostedFile Item { get; set; }
        public string Token { get; set; }
    }
}
