using System;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IRestClient
    {
        Uri BuildUri(string handler, string query = "");

        Task<T> GetAsync<T>(Uri requestUri);

        T PostFile<T>(Uri requestUri, IHttpPostedFile file);

        Task<T> PostAsync<T>(Uri requestUri, object item = null, string contentType = "application/json");

        Task<T> PutAsync<T>(Uri requestUri, object item = null, string contentType = "application/json");

        Task DeleteAsync(Uri requestUri);
    }
}