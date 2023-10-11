using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, PagerParameters parameters = null,
            CancellationToken cancellationToken = default)
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

            return client.GetAsync(requestUri, cancellationToken);
        }

        /// https://developer.zendesk.com/api-reference/ticketing/tickets/ticket_audits/#pagination
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, CursorPagerVariant pager,
            CancellationToken cancellationToken = default)
        {
            if (pager == null)
                pager = new CursorPagerVariant();

            if (!string.IsNullOrEmpty(pager.Cursor))
            {
                var encodedCursor = Uri.EscapeDataString(pager.Cursor);

                if (requestUri.Contains("?"))
                    requestUri += $"&cursor={encodedCursor}";
                else
                    requestUri += $"?cursor={encodedCursor}";
            }

            if (requestUri.Contains("?"))
                requestUri += $"&limit={pager.ResultsLimit}";
            else
                requestUri += $"?limit={pager.ResultsLimit}";

            return client.GetAsync(requestUri, cancellationToken);
        }

        // https://developer.zendesk.com/api-reference/ticketing/introduction/#pagination
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            if (pager == null) pager = new CursorPager();

            if (requestUri.Contains("?"))
            {
                requestUri += $"&page[size]={pager.Size}";
            }
            else
            {
                requestUri += $"?page[size]={pager.Size}";
            }

            if (!string.IsNullOrEmpty(pager.AfterCursor)) requestUri += $"&page[after]={pager.AfterCursor}";

            if (!string.IsNullOrEmpty(pager.BeforeCursor)) requestUri += $"&page[before]={pager.BeforeCursor}";

            return client.GetAsync(requestUri, cancellationToken);
        }
    }
}
