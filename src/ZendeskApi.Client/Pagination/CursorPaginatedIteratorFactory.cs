using System;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Pagination
{
    public interface ICursorPaginatedIteratorFactory
    {
        CursorPaginatedIterator<T> Create<T>(ICursorPagination<T> response);
    }

    public class CursorPaginatedIteratorFactory : ICursorPaginatedIteratorFactory
    {
        private readonly IZendeskApiClient zendeskApiClient;

        public CursorPaginatedIteratorFactory(IZendeskApiClient _zendeskApiClient)
        {
            zendeskApiClient = _zendeskApiClient;
        }

        public CursorPaginatedIterator<T> Create<T>(ICursorPagination<T> response)
        {
            return new CursorPaginatedIterator<T>(response, zendeskApiClient);
        }
    }
}

