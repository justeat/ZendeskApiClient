using System.IO;

namespace ZendeskApi.Contracts.Requests
{
    public class UploadRequest 
    {
        public string FileName { get; set; }
        public Stream InputStream { get; set; } // Turn to byte array?
    }
}
