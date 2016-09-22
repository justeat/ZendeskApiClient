using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        IHttpResponse Get(IHttpRequest request, string resource = "", string operation = "");

        Task<IHttpResponse> GetAsync(IHttpRequest request, string resource = "", string operation = "");

        IHttpResponse Post(IHttpRequest request, string resource = "", string operation = "");

        IHttpResponse Post(IHttpRequest request, IHttpPostedFile fileBase, string resource = "", string operation = "");

        Task<IHttpResponse> PostAsync(IHttpRequest request, string resource = "", string operation = "");

        IHttpResponse Put(IHttpRequest request, string resource = "", string operation = "");

        Task<IHttpResponse> PutAsync(IHttpRequest request, string resource = "", string operation = "");

        IHttpResponse Delete(IHttpRequest request, string resource = "", string operation = "");

        Task<IHttpResponse> DeleteAsync(IHttpRequest request, string resource = "", string operation = "");
    }
}