using System.IO;

namespace ZendeskApi.Contracts.Models
{
    public interface IHttpPostedFile
    {
        string ContentType { get; }

        string FileName { get; }

        Stream InputStream { get; }
    }
}
