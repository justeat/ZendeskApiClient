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
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class TicketFieldsResourceSampleSite : SampleSite
    {
        private class State {
            public IDictionary<long, TicketField> TicketFields = new Dictionary<long, TicketField>();
        }
        
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.TicketFields.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var ticketField = state.TicketFields.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(ticketField);
                    })
                    .MapGet("api/v2/ticket_fields", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketFieldsResponse { TicketFields = state.TicketFields.Values });
                    })
                    .MapPost("api/v2/ticket_fields", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.ReadAs<TicketField>();

                        if (ticket.Tag != null && ticket.Tag.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        ticket.Id = long.Parse(Rand.Next().ToString());
                        state.TicketFields.Add(ticket.Id.Value, ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(ticket);
                    })
                    .MapPut("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var ticket = req.Body.ReadAs<TicketField>();

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.TicketFields[id] = ticket;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(state.TicketFields[id]);
                    })
                    .MapDelete("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.TicketFields.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    })
                    ;
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;
        
        public TicketFieldsResourceSampleSite(string resource)
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
