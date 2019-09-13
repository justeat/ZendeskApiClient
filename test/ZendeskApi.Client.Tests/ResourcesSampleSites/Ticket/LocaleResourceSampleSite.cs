using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class LocaleResourceSampleSite : SampleSite<Locale>
    {
        public LocaleResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<Locale> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Locale
                {
                    Id = i,
                    Name = $"name.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/locales/current", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Locale>>();

                        var item = state.Items.First().Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new LocaleResponse
                        {
                            Locale = item
                        });
                    })
                    .MapGet("api/v2/locales/detect_best_locale", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Locale>>();

                        var item = state.Items.First().Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new LocaleResponse
                        {
                            Locale = item
                        });
                    })
                    .MapGet("api/v2/locales/public", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<LocaleListResponse, Locale>(
                            req,
                            resp,
                            items => new LocaleListResponse
                            {
                                Locales = items.ToArray()
                            });
                    })
                    .MapGet("api/v2/locales/agent", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<LocaleListResponse, Locale>(
                            req,
                            resp,
                            items => new LocaleListResponse
                            {
                                Locales = items.ToArray()
                            });
                    })
                    .MapGet("api/v2/locales/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<LocaleResponse, Locale>(
                            req,
                            resp,
                            routeData,
                            item => new LocaleResponse
                            {
                                Locale = item
                            });
                    })
                    .MapGet("api/v2/locales", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<LocaleListResponse, Locale>(
                            req,
                            resp,
                            items => new LocaleListResponse
                            {
                                Locales = items.ToArray()
                            });
                    })
                    ;
            }
        }
    }
}