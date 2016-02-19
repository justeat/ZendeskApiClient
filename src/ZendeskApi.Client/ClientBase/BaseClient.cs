using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Logging;
using ZendeskApi.Client.Serialization;

namespace ZendeskApi.Client.ClientBase
{
    public class BaseClient : BaseClientImplementation, IBaseClient
    {
        public BaseClient(Uri baseUri, DefaultConfiguration configuration, IOptionalDependencies dependencies)
            : base(baseUri, configuration, dependencies)
        {
        }

        public T Get<T>(Uri requestUri)
        {
            return Get<T>(requestUri, null, null);
        }

        public Task<T> GetAsync<T>(Uri requestUri)
        {
            return GetAsync<T>(requestUri, null, null);
        }

        public T Post<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return Post<T>(requestUri, item, contentType, null, null);
        }

        public Task<T> PostAsync<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return PostAsync<T>(requestUri, item, contentType, null, null);
        }

        public T Put<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return Put<T>(requestUri, item, contentType, null, null);
        }

        public Task<T> PutAsync<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return PutAsync<T>(requestUri, item, contentType, null, null);
        }

        public T Patch<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return Patch<T>(requestUri, item, contentType, null, null);
        }

        public Task<T> PatchAsync<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return PatchAsync<T>(requestUri, item, contentType, null, null);
        }

        public void Delete(Uri requestUri, object item = null, string contentType = null)
        {
            Delete<object>(requestUri, item, contentType);
        }

        public Task DeleteAsync(Uri requestUri, object item = null, string contentType = null)
        {
            return DeleteAsync<object>(requestUri, item, contentType);
        }

        public T Delete<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return Delete<T>(requestUri, item, contentType, null, null);
        }

        public Task<T> DeleteAsync<T>(Uri requestUri, object item = null, string contentType = null)
        {
            return DeleteAsync<T>(requestUri, item, contentType, null, null);
        }
    }
}
