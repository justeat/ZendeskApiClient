/*using System;
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
using ZendeskApi.Client.Models.Tickets;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
using Ticket = ZendeskApi.Client.Models.Ticket;

namespace ZendeskApi.Client.Tests
{
    public class TicketResourceSampleSite : SampleSite
    {
        private class State {
            public IDictionary<long, Ticket> Tickets = new Dictionary<long, Ticket>();
            public IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/tickets/show_many", (req, resp, routeData) =>
                    {
                        var ids = req.Query["ids"].ToString().Split(',').Select(long.Parse);
                        var pager = new Pager(int.Parse(req.Query["page"]), int.Parse(req.Query["per_page"]), 100);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => ids.Contains(x.Key)).Select(p => p.Value)
                            .Skip(pager.GetStartIndex())
                            .Take(pager.PageSize);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
                    })
                    .MapGet("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Tickets.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var ticket = state.Tickets.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(ticket);
                    })
                    .MapGet("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var pager = new Pager(int.Parse(req.Query["page"]), int.Parse(req.Query["per_page"]), 100);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Skip(pager.GetStartIndex())
                            .Take(pager.PageSize);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
                    })
                    .MapGet("api/v2/tickets/{id}/comments", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var comments = state.TicketComments.ContainsKey(id) ? state.TicketComments[id] : new List<TicketComment>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketCommentsResponse { Item = comments });
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
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
                    })
                    .MapGet("api/v2/users/{id}/tickets/requested", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.RequesterId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
                    })
                    .MapGet("api/v2/organizations/{id}/tickets", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Where(x => x.Value.OrganisationId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsResponse { Item = tickets });
                    })
                    .MapPost("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.ReadAs<Ticket>();

                        if (ticket.Tags != null && ticket.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        ticket.Id = long.Parse(RAND.Next().ToString());
                        ticket.Url = new Uri("https://company.zendesk.com/api/v2/tickets/" + ticket.Id + ".json");
                        state.Tickets.Add(ticket.Id, ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(ticket);
                    })
                    .MapPost("api/v2/tickets/create_many", (req, resp, routeData) =>
                    {
                        var tickets = req.Body.ReadAs<TicketsRequest<CreateTicketRequest>>().Item;

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        
                        foreach (var ticket in tickets)
                        {
                            ticket.Id = long.Parse(RAND.Next().ToString());
                            ticket.Url = new Uri("https://company.zendesk.com/api/v2/tickets/" + ticket.Id + ".json");
                            state.Tickets.Add(ticket.Id.Value, ticket);
                        }

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        return resp.WriteAsJson(new JobStatus
                            {
                                Id = Guid.NewGuid().ToString().Replace("-", ""),
                                Items = tickets.Select(x => new JobStatusResult { Id = x.Id.Value, Title = x.Subject })
                            }
                        );
                    })
                    .MapPut("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var ticket = req.Body.ReadAs<Ticket>();

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (ticket.Comment != null)
                        {
                            ticket.Comment.Id = long.Parse(RAND.Next().ToString());

                            if (state.TicketComments.ContainsKey(id))
                            {
                                state.TicketComments[id].Add(ticket.Comment);
                            }
                            else
                            {
                                state.TicketComments.Add(id, new List<TicketComment> { ticket.Comment });
                            }
                            ticket.Comment = null;
                        }

                        state.Tickets[id] = ticket;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(state.Tickets[id]);
                    })
                    .MapDelete("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Tickets.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
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
*/