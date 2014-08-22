using System.Threading.Tasks;

namespace JustEat.ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        IHttpResponse Get(IHttpRequest request);

        Task<IHttpResponse> GetAsync(IHttpRequest request);

        IHttpResponse Post(IHttpRequest request);

        Task<IHttpResponse> PostAsync(IHttpRequest request);

        IHttpResponse Put(IHttpRequest request);

        Task<IHttpResponse> PutAsync(IHttpRequest request);

        IHttpResponse Delete(IHttpRequest request);

        Task<IHttpResponse> DeleteAsync(IHttpRequest request);
    }
}