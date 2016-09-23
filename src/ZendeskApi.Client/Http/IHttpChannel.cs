using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        IHttpResponse Get(IHttpRequest request, string clientName,string resourceName);

        Task<IHttpResponse> GetAsync(IHttpRequest request, string clientName, string resourceName);

        IHttpResponse Post(IHttpRequest request, string clientName, string resourceName);

        IHttpResponse Post(IHttpRequest request, IHttpPostedFile fileBase, string clientName, string resourceName);

        Task<IHttpResponse> PostAsync(IHttpRequest request, string clientName, string resourceName);

        IHttpResponse Put(IHttpRequest request, string clientName, string resourceName);

        Task<IHttpResponse> PutAsync(IHttpRequest request, string clientName, string resourceName);

        IHttpResponse Delete(IHttpRequest request, string clientName, string resourceName);

        Task<IHttpResponse> DeleteAsync(IHttpRequest request, string clientName, string resourceName);
    }
}