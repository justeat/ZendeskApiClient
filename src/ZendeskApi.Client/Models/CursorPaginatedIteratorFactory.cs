using System;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    public class CursorPaginatedIteratorFactory<T>
    {
        private static IZendeskApiClient ApiClient;

        public CursorPaginatedIteratorFactory(IZendeskApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public static CursorPaginatedIterator<T> GetPaginatedIterator(ICursorPagination<T> response)
        {
            return new CursorPaginatedIterator<T>(response, ApiClient);
        }
    }
}

