using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal static class RequestHelper
    {
        public static Task List<TResponse, TModel>(
            HttpRequest req, 
            HttpResponse resp,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<State<TModel>>();

            var items = state.Items
                .Select(x => x.Value)
                .ToList();

            if (req.Query.ContainsKey("page") &&
                req.Query.ContainsKey("per_page") &&
                int.TryParse(req.Query["page"].ToString(), out var page) &&
                int.TryParse(req.Query["per_page"].ToString(), out var size))
            {
                if (page == int.MaxValue && size == int.MaxValue)
                {
                    resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    return Task.FromResult(resp);
                }

                items = items
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(items));
        }

        public static Task FilteredList<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            string filterValue,
            Func<long, IList<TModel>, IList<TModel>> filter,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            var id = long.Parse(filterValue);

            if (id == int.MaxValue)
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                return Task.FromResult(resp);
            }

            if (id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<State<TModel>>();

            var items = filter(
                id,
                state
                    .Items
                    .Select(x => x.Value)
                    .ToList()
            );

            if (req.Query.ContainsKey("page") &&
                req.Query.ContainsKey("per_page") &&
                int.TryParse(req.Query["page"].ToString(), out var page) &&
                int.TryParse(req.Query["per_page"].ToString(), out var size))
            {
                items = items
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(items));
        }

        public static Task Many<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            Func<TModel, long> idAccessor,
            Func<TModel, string> externalIdAccessor,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<State<TModel>>();

            IList<TModel> items = new List<TModel>();

            var idsQuery = req.Query.ContainsKey("ids")
                ? req.Query["ids"].ToString()
                : req.Query["external_ids"].ToString();

            var ids = idsQuery
                .Split(',')
                .Select(long.Parse)
                .ToList();

            if (ids.First() == long.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            if (req.Query.ContainsKey("ids"))
            {
                items = state.Items
                    .Select(x => x.Value)
                    .Where(x => ids.Contains(idAccessor(x)))
                    .ToList();
            }
            else if (req.Query.ContainsKey("external_ids"))
            {
                items = state.Items
                    .Select(x => x.Value)
                    .Where(x => ids.Contains(long.Parse(externalIdAccessor(x))))
                    .ToList();
            }

            if (req.Query.ContainsKey("page") &&
                req.Query.ContainsKey("per_page") &&
                int.TryParse(req.Query["page"].ToString(), out var page) &&
                int.TryParse(req.Query["per_page"].ToString(), out var size))
            {
                items = items
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(items));
        }

        public static Task GetById<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
        {
            var id = long.Parse(routeData.Values["id"].ToString());

            var state = req.HttpContext.RequestServices.GetRequiredService<State<TModel>>();

            if (id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            if (!state.Items.ContainsKey(id))
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                return Task.CompletedTask;
            }

            var item = state.Items.Single(x => x.Key == id).Value;

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(item));
        }

        public static Task Update<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            TModel item,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
        {
            var id = long.Parse(routeData.Values["id"].ToString());

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<State<TModel>>();

            if (id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            if (!state.Items.ContainsKey(id))
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                return Task.CompletedTask;
            }

            state.Items[id] = item;

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(item));
        }

        public static Task Delete<TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            HttpStatusCode returnCode = HttpStatusCode.NoContent)
            where TModel : class
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<State<TModel>>();

            var id = long.Parse(routeData.Values["id"].ToString());

            if (id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            resp.StatusCode = (int) returnCode;

            state.Items.Remove(id);

            return Task.FromResult(resp);
        }
    }
}
