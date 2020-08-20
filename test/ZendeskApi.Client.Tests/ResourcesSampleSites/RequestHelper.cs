using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
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
            return List<TResponse, TModel, State<TModel>>(
                req,
                resp,
                outputFunc);
        }

        public static Task List<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

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

            if (req.Query.ContainsKey("limit") && 
                int.TryParse(req.Query["limit"].ToString(), out var limit))
            {
                if (limit == int.MaxValue)
                {
                    resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    return Task.FromResult(resp);
                }

                items = items
                    .Skip(0)
                    .Take(limit)
                    .ToList();
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(items));
        }

        public static Task FilteredList<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            string filterValue,
            Func<long, IEnumerable<TModel>, IEnumerable<TModel>> filter,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            return FilteredList<TResponse, TModel, State<TModel>>(
                req,
                resp,
                filterValue,
                filter,
                outputFunc);
        }

        public static Task FilteredList<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            string filterValue,
            Func<long, IEnumerable<TModel>, IEnumerable<TModel>> filter,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
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
                .GetRequiredService<TState>();

            var items = filter(
                id,
                state
                    .Items
                    .Select(x => x.Value)
                    .ToList()
            ).ToList();

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

        public static Task Many<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            Func<TModel, long> idAccessor,
            Func<TModel, string> externalIdAccessor,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            return Many<TResponse, TModel, State<TModel>>(
                req,
                resp,
                idAccessor,
                externalIdAccessor,
                outputFunc);
        }

        public static Task Many<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            Func<TModel, long> idAccessor,
            Func<TModel, string> externalIdAccessor,
            Func<IList<TModel>, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

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
            return GetById<TResponse, TModel, State<TModel>>(
                req,
                resp,
                routeData,
                outputFunc);
        }

        public static Task GetById<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var id = long.Parse(routeData.Values["id"].ToString());

            var state = req.HttpContext.RequestServices.GetRequiredService<TState>();

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

        public static Task Create<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, long?> idAccessor,
            TModel item,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
        {
            return Create<TResponse, TModel, State<TModel>>(
                req,
                resp,
                routeData,
                idAccessor,
                item,
                outputFunc);
        }

        public static Task Create<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, long?> idAccessor,
            TModel item,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var id = idAccessor(item);

            if (!id.HasValue || id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.CompletedTask;
            }

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

            state.Items.Add(id.Value, item);

            resp.StatusCode = (int)HttpStatusCode.Created;

            return resp.WriteAsJson(outputFunc(item));
        }

        public static Task CreateMany<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, long?> idAccessor,
            IEnumerable<TModel> items,
            Func<IEnumerable<TModel>, TResponse> outputFunc)
            where TModel : class
        {
            return CreateMany<TResponse, TModel, State<TModel>>(
                req,
                resp,
                routeData,
                idAccessor,
                items,
                outputFunc);
        }

        public static Task CreateMany<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            Func<TModel, long?> idAccessor,
            IEnumerable<TModel> items,
            Func<IEnumerable<TModel>, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var itemsList = items
                .ToList();

            if (itemsList.Any(item => !idAccessor(item).HasValue || idAccessor(item) == int.MinValue))
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.CompletedTask;
            }

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

            foreach (var item in itemsList)
            {
                var id = idAccessor(item);

                if (id.HasValue && !state.Items.ContainsKey(id.Value))
                    state.Items.Add(id.Value, item);
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return resp.WriteAsJson(outputFunc(itemsList));
        }

        public static Task Update<TResponse, TModel>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            TModel item,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
        {
            return Update<TResponse, TModel, State<TModel>>(
                req,
                resp,
                routeData,
                item,
                outputFunc);
        }

        public static Task Update<TResponse, TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            TModel item,
            Func<TModel, TResponse> outputFunc)
            where TModel : class
            where TState : State<TModel>
        {
            var id = long.Parse(routeData.Values["id"].ToString());

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

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
            return Delete<TModel, State<TModel>>(
                req,
                resp,
                routeData,
                returnCode);
        }

        public static Task Delete<TModel, TState>(
            HttpRequest req,
            HttpResponse resp,
            RouteData routeData,
            HttpStatusCode returnCode = HttpStatusCode.NoContent)
            where TModel : class
            where TState : State<TModel>
        {
            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

            var id = long.Parse(routeData.Values["id"].ToString());

            if (id == int.MinValue)
            {
                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Task.FromResult(resp);
            }

            resp.StatusCode = (int)returnCode;

            state.Items.Remove(id);

            return Task.FromResult(resp);
        }

        public static Task DeleteMany<TModel>(
            HttpRequest req,
            HttpResponse resp)
            where TModel : class
        {
            return DeleteMany<TModel, State<TModel>>(
                req,
                resp);
        }

        public static Task DeleteMany<TModel, TState>(
            HttpRequest req,
            HttpResponse resp)
            where TModel : class
            where TState : State<TModel>
        {
            var idParameterValue = req
                .Query["ids"]
                .First();

            if (!idParameterValue.Contains(","))
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Task.CompletedTask;
            }

            var theIds = idParameterValue
                .Split(',')
                .Select(x => long.Parse(x.Trim()))
                .ToList();

            var state = req
                .HttpContext
                .RequestServices
                .GetRequiredService<TState>();

            foreach (var anId in theIds)
            {
                state.Items.Remove(anId);
            }

            resp.StatusCode = (int)HttpStatusCode.OK;

            return Task.CompletedTask;
        }
    }
}
