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
    internal class UserIdentitiesResourceSampleSite : SampleSite<State<Tuple<long, long>, UserIdentity>, Tuple<long, long>, UserIdentity>
    {
        public UserIdentitiesResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/{userId}/identities/{identityid}", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());
                        var identityid = long.Parse(routeData.Values["identityid"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();

                        if (!state.Items.ContainsKey(new Tuple<long, long>(userId, identityid)))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var identity = state.Items[new Tuple<long, long>(userId, identityid)];

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(identity);
                    })
                    .MapGet("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();

                        if (state.Items.All(x => x.Key.Item1 != userId))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var identities = state.Items.Where(x => x.Key.Item1 == userId).Select(x => x.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UserIdentitiesResponse { Identities = identities });
                    })
                    .MapPost("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.ReadAs<UserIdentity>();

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();

                        identity.Id = long.Parse(Rand.Next().ToString());
                        state.Items.Add(new Tuple<long, long>(userId, identity.Id.Value), identity);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapPost("api/v2/end_users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.ReadAs<UserIdentity>();

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();

                        identity.Id = long.Parse(Rand.Next().ToString());
                        state.Items.Add(new Tuple<long, long>(userId, identity.Id.Value), identity);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapPut("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.ReadAs<UserIdentity>();

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();

                        state.Items[new Tuple<long, long>(userId, identity.Id.Value)] = identity;

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapDelete("api/v2/users/{userid}/identities/{identityid}", (req, resp, routeData) =>
                    {
                        var userid = long.Parse(routeData.Values["userid"].ToString());
                        var identityid = long.Parse(routeData.Values["identityid"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Tuple<long, long>, UserIdentity>>();
                        
                        state.Items.Remove(new Tuple<long, long>(userid, identityid));

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    })
                    ;
            }
        }
    }
}
