using System;
using System.Collections.Generic;
using System.Dynamic;
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
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class TicketResourceSampleSite : SampleSite
    {
        private class State {
            public IDictionary<long, dynamic> Tickets = new Dictionary<long, dynamic>();
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
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
                        return resp.WriteAsJson(new TicketsListResponse { Item = tickets });
                    })
                    .MapPost("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.ReadAs<dynamic>();

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        ticket.id = long.Parse(Rand.Next().ToString());
                        ticket.url = new Uri($"https://company.zendesk.com/api/v2/tickets/{ticket.Id}.json");

                        state.Tickets.Add(ticket.Id, ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        return resp.WriteAsJson((object)ticket);
                    })
                    .MapPut("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Tickets.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return resp.WriteAsJson(new object());
                        }

                        var ticket = req.Body.ReadAs<dynamic>();

                        if (ticket.comment != null)
                        {
                            ticket.comment.Id = long.Parse(Rand.Next().ToString());

                            if (state.TicketComments.ContainsKey(id))
                            {
                                state.TicketComments[id].Add(ticket.comment);
                            }
                            else
                            {
                                state.TicketComments.Add(id, new List<TicketComment> { ticket.comment });
                            }
                            ticket.Comment = null;
                        }

                        var sourceTicket = state.Tickets[id];
                        foreach (var memberName in ticket.GetDynamicMemberNames())
                        {
                            sourceTicket[memberName] = ticket[memberName];
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson((object)sourceTicket);
                    })
                    .MapDelete("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Tickets.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    }) ;
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
