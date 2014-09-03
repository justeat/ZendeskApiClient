using System;
using System.Threading.Tasks;

namespace ZendeskApi.Client.Http
{
    public interface IRestClient
    {
        Uri BuildUri(string handler, string query = "");

        T Get<T>(Uri requestUri);

        Task<T> GetAsync<T>(Uri requestUri);

        T Post<T>(Uri requestUri, object item = null, string contentType = "application/json");

        Task<T> PostAsync<T>(Uri requestUri, object item = null, string contentType = "application/json");

        T Put<T>(Uri requestUri, object item = null, string contentType = "application/json");

        Task<T> PutAsync<T>(Uri requestUri, object item = null, string contentType = "application/json");

        void Delete(Uri requestUri);

        Task DeleteAsync(Uri requestUri);
    }
}