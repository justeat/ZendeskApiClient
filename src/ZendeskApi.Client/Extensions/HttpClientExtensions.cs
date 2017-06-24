using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(
            this HttpClient client, string requestUri, PagerParameters parameters = null)
        {
            var pager = new Pager(parameters?.Page, parameters?.PageSize, 100);

            var pagination = $"per_page={pager.PageSize}&page={pager.Page}";

            if (requestUri.Contains("?"))
            {
                requestUri += "&" + pagination;
            }
            else
            {
                requestUri += "?" + pagination;
            }

            return client.GetAsync(requestUri);
        }
    }
}
