using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        IHttpResponse Get(IHttpRequest request, string clientName, string resourceName, string operation);

        Task<IHttpResponse> GetAsync(IHttpRequest request, string clientName, string resourceName, string operation);

        IHttpResponse Post(IHttpRequest request, string clientName, string resourceName, string operation);

        IHttpResponse Post(IHttpRequest request, IHttpPostedFile fileBase, string clientName, string resourceName, string operation);

        Task<IHttpResponse> PostAsync(IHttpRequest request, string clientName, string resourceName, string operation);

        IHttpResponse Put(IHttpRequest request, string clientName, string resourceName, string operation);

        Task<IHttpResponse> PutAsync(IHttpRequest request, string clientName, string resourceName, string operation);

        IHttpResponse Delete(IHttpRequest request, string clientName, string resourceName, string operation);

        Task<IHttpResponse> DeleteAsync(IHttpRequest request, string clientName, string resourceName, string operation);
    }
}