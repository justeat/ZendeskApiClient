using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UserFieldsResourceSampleSite : SampleSite<UserField>
    {
        public UserFieldsResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserField>>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var userField = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UserFieldResponse { UserField = userField });
                    })
                    .MapGet("api/v2/user_fields", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserField>>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UserFieldsResponse { UserFields = state.Items.Values });
                    })
                    .MapPost("api/v2/user_fields", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserField>();

                        if (user.Tag != null && user.Tag.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserField>>();

                        user.Id = long.Parse(Rand.Next().ToString());
                        state.Items.Add(user.Id.Value, user);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(user);
                    })
                    .MapPut("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var user = req.Body.ReadAs<UserField>();

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserField>>();

                        state.Items[id] = user;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(state.Items[id]);
                    })
                    .MapDelete("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserField>>();

                        state.Items.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    })
                    ;
            }
        }
    }
}
