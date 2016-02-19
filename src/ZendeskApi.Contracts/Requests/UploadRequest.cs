using System.Web;

namespace ZendeskApi.Contracts.Requests
{
    public class UploadRequest 
    {
        public HttpPostedFileBase Item { get; set; }
        public string Token { get; set; }
    }
}
