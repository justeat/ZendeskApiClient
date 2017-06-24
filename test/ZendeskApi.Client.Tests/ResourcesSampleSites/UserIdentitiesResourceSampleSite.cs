using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class UserIdentitiesResourceSampleSite : SampleSite
    {
        private class State
        {
            public IDictionary<Tuple<long, long>, UserIdentity> Identities = new Dictionary<Tuple<long, long>, UserIdentity>();
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/{userId}/identities/{identityid}", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());
                        var identityid = long.Parse(routeData.Values["identityid"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Identities.ContainsKey(new Tuple<long, long>(userId, identityid)))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var identity = state.Identities[new Tuple<long, long>(userId, identityid)];

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(identity);
                    })
                    .MapGet("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Identities.Any(x => x.Key.Item1 == userId))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var identities = state.Identities.Where(x => x.Key.Item1 == userId).Select(x => x.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UserIdentitiesResponse { Item = identities });
                    })
                    .MapPost("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.Deserialize<UserIdentityRequest>().Item;

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        identity.Id = long.Parse(RAND.Next().ToString());
                        state.Identities.Add(new Tuple<long, long>(userId, identity.Id.Value), identity);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapPost("api/v2/end_users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.Deserialize<UserIdentityRequest>().Item;

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        identity.Id = long.Parse(RAND.Next().ToString());
                        state.Identities.Add(new Tuple<long, long>(userId, identity.Id.Value), identity);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapPut("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        var identity = req.Body.Deserialize<UserIdentityRequest>().Item;

                        var userId = long.Parse(routeData.Values["userId"].ToString());

                        if (identity.Value != null && identity.Value.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Identities[new Tuple<long, long>(userId, identity.Id.Value)] = identity;

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(identity);
                    })
                    .MapDelete("api/v2/users/{userid}/identities/{identityid}", (req, resp, routeData) =>
                    {
                        var userid = long.Parse(routeData.Values["userid"].ToString());
                        var identityid = long.Parse(routeData.Values["identityid"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        
                        state.Identities.Remove(new Tuple<long, long>(userid, identityid));

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    })
                    ;
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public UserIdentitiesResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => {
                    services.AddSingleton<State>((_) => new State());
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);
            _client = _server.CreateClient();

            RefreshClient(resource);
        }

        public override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : resource;
        }

        public Uri BaseUri
        {
            get { return Client.BaseAddress; }
        }

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
