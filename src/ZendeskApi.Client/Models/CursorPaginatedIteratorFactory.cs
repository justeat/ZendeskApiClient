using System;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    public interface ICursorPaginatedIteratorFactory
    {
        CursorPaginatedIterator<T> Create<T>(ICursorPagination<T> response);
    }

    public class CursorPaginatedIteratorFactory : ICursorPaginatedIteratorFactory
    {
        private static IServiceProvider serviceProvider;

        public CursorPaginatedIteratorFactory(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public CursorPaginatedIterator<T> Create<T>(ICursorPagination<T> response)
        {
            return new CursorPaginatedIterator<T>(response, serviceProvider.GetRequiredService<IZendeskApiClient>());
        }
    }
}

