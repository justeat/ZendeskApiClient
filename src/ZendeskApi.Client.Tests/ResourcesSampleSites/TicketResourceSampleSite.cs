using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests
{
    public class TicketResourceSampleSite : SampleSite
    {
        private class State {
            public IDictionary<long, Ticket> Tickets = new Dictionary<long, Ticket>();
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/tickets/show_many", (req, resp, routeData) =>
                    {
                        var ids = req.Query["ids"].ToString().Split(',').Select(long.Parse);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state.Tickets.Where(x => ids.Contains(x.Key)).Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = tickets }));
                    })
                    .MapGet("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var ticket = state.Tickets.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = ticket }));
                    })
                    .MapGet("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = state.Tickets.Values }));
                    })
                    .MapGet("api/v2/users/{id}/tickets/assigned", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.AssigneeId.HasValue)
                            .Where(x => x.Value.AssigneeId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = tickets }));
                    })
                    .MapGet("api/v2/users/{id}/tickets/ccd", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.CollaboratorIds.Contains(id))
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = tickets }));
                    })
                    .MapGet("api/v2/users/{id}/tickets/requested", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.RequesterId.HasValue)
                            .Where(x => x.Value.RequesterId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = tickets }));
                    })
                    .MapGet("api/v2/organizations/{id}/tickets", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.OrganisationId.HasValue)
                            .Where(x => x.Value.OrganisationId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketsResponse { Item = tickets }));
                    })
                    .MapPost("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.Deserialize<TicketRequest>().Item;

                        if (ticket.Tags != null && ticket.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        ticket.Id = long.Parse(new Random().Next().ToString());
                        state.Tickets.Add(ticket.Id.Value, ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = ticket }));
                    })
                    .MapPost("api/v2/tickets/create_many", (req, resp, routeData) =>
                    {
                        var tickets = req.Body.Deserialize<TicketsRequest>().Item;

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var rand = new Random();

                        foreach (var ticket in tickets)
                        {
                            ticket.Id = long.Parse(rand.Next().ToString());
                            state.Tickets.Add(ticket.Id.Value, ticket);
                        }

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        return resp.WriteAsync(JsonConvert.SerializeObject(new JobStatusResponse
                        {
                            Item = new JobStatus
                            {
                                Id = Guid.NewGuid().ToString().Replace("-", ""),
                                Items = tickets.Select(x => new JobStatusResult { Id = x.Id.Value, Title = x.Subject })
                            }
                        }));
                    })
                    .MapPut("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var ticket = req.Body.Deserialize<TicketRequest>().Item;

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Tickets[id] = ticket;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = state.Tickets[id] }));
                    })
                    ;
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;
        
        public TicketResourceSampleSite(string resource)
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
