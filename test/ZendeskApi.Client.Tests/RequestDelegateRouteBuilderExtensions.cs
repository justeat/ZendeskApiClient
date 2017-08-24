// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ZendeskApi.Client.Tests
{
    public static class RequestDelegateRouteBuilderExtensions
    {
        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP GET requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The <see cref="RequestDelegate"/> route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapGet(this IRouteBuilder builder, string template, RequestDelegate handler)
        {
            return builder.MapVerb("GET", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP GET requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapGet(
            this IRouteBuilder builder,
            string template,
            Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder.MapVerb("GET", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP requests for the given
        /// <paramref name="verb"/>, <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="verb">The HTTP verb allowed by the route.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapVerb(
            this IRouteBuilder builder,
            string verb,
            string template,
            Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            RequestDelegate requestDelegate = (httpContext) =>
            {
                return handler(httpContext.Request, httpContext.Response, httpContext.GetRouteData());
            };

            return builder.MapVerb(verb, template, requestDelegate);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP POST requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapPost(
            this IRouteBuilder builder,
            string template,
            Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder.MapVerb("POST", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP POST requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The <see cref="RequestDelegate"/> route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapPost(this IRouteBuilder builder, string template, RequestDelegate handler)
        {
            return builder.MapVerb("POST", template, handler);
        }


        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP PUT requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapPut(
            this IRouteBuilder builder,
            string template,
            Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder.MapVerb("PUT", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP DELETE requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapDelete(
            this IRouteBuilder builder,
            string template,
            Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder.MapVerb("DELETE", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP DELETE requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The <see cref="RequestDelegate"/> route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapDelete(this IRouteBuilder builder, string template, RequestDelegate handler)
        {
            return builder.MapVerb("DELETE", template, handler);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> that only matches HTTP PUT requests for the given
        /// <paramref name="template"/>, and <paramref name="handler"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IRouteBuilder"/>.</param>
        /// <param name="template">The route template.</param>
        /// <param name="handler">The <see cref="RequestDelegate"/> route handler.</param>
        /// <returns>A reference to the <paramref name="builder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapPut(this IRouteBuilder builder, string template, RequestDelegate handler)
        {
            return builder.MapVerb("PUT", template, handler);
        }

        private static IInlineConstraintResolver GetConstraintResolver(IRouteBuilder builder)
        {
            return builder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();
        }
    }
}