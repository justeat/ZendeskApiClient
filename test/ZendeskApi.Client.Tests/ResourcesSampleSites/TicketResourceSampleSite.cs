using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class TicketResourceSampleSite : SampleSite
    {
        private class State {
            public readonly IDictionary<long, Ticket> Tickets = new Dictionary<long, Ticket>();
            public readonly IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
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
                            .Where(x => ids.Contains(x.Key))
                            .Select(p => p.Value)
                            .Skip(pager.GetStartIndex())
                            .Take(pager.PageSize);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
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
                        return resp.WriteAsJson(new TicketResponse{Ticket = ticket});
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
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
                    })
                    .MapGet("api/v2/tickets/{id}/comments", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var comments = state.TicketComments.ContainsKey(id) ? state.TicketComments[id] : new List<TicketComment>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketCommentsResponse { Comments = comments });
                    })
                    .MapGet("api/v2/users/{id}/tickets/assigned", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Where(x => x.AssigneeId.HasValue && x.AssigneeId == id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
                    })
                    .MapGet("api/v2/users/{id}/tickets/ccd", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Where(x => x.CollaboratorIds.Contains(id));

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
                    })
                    .MapGet("api/v2/users/{id}/tickets/requested", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Where(x => x.RequesterId.HasValue && x.RequesterId == id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
                    })
                    .MapGet("api/v2/organizations/{id}/tickets", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Where(x => x.OrganisationId == id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketsListResponse { Tickets = tickets });
                    })
                    .MapPost("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var ticketRequest = req.Body.ReadAs<TicketRequest<TicketCreateRequest>>();
                        var ticket = ticketRequest.Ticket;

                        if (ticket.Tags?.Contains("error") ?? false)
                        {
                            resp.StatusCode = (int)HttpStatusCode.BadRequest;
                            return resp.WriteAsJson(new object());
                        }

                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        var ticketResponse = mapper.Map<TicketResponse>(ticket);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        ticketResponse.Ticket.Id = long.Parse(Rand.Next().ToString());
                        ticketResponse.Ticket.Url = new Uri($"https://company.zendesk.com/api/v2/tickets/{ticketResponse.Ticket.Id}.json");


                        HandleTicketComment(ticket.Comment, state, ticketResponse.Ticket.Id);

                        state.Tickets.Add(ticketResponse.Ticket.Id, ticketResponse.Ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(ticketResponse);
                    })
                    .MapPut("api/v2/tickets/update_many.json", (req, resp, routeData) =>
                    {
                        var theIds = req.Query["ids"]
                            .SelectMany(q => q.Split(','))
                            .Select(long.Parse);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var ticketRequestWrapper = req.Body.ReadAs<TicketListRequest<TicketUpdateRequest>>();

                        var ticketsById = ticketRequestWrapper.Tickets.ToDictionary(t => t.Id, t => t);

                        foreach (var id in theIds.Where(id => ticketsById.ContainsKey(id)))
                        {
                            var ticket = ticketsById[id];
                            HandleTicketComment(ticket.Comment, state, ticket.Id);
                        }

                        var status = new JobStatusResult
                        {
                            Id = Rand.Next()
                        };

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(status);
                    })
                    .MapPut("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Tickets.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsJson(new object());
                        }

                        var ticketRequestWrapper = req.Body.ReadAs<TicketRequest<TicketUpdateRequest>>();
                        var ticketRequest = ticketRequestWrapper.Ticket;

                        HandleTicketComment(ticketRequest.Comment, state, ticketRequest.Id);

                        var ticketResponse = new TicketResponse {Ticket = state.Tickets[id] };
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        mapper.Map(ticketRequest, ticketResponse);

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(ticketResponse);
                    })
                    .MapDelete("api/v2/tickets/destroy_many.json", (req, resp, routeData) =>
                    {
                        var idParameterValue = req.Query["ids"].First().ToString();

                        if (!idParameterValue.Contains(","))
                        {
                            resp.StatusCode = 500;
                            return Task.CompletedTask;
                        }

                        var theIds = idParameterValue
                            .Split(',')
                            .Select(x => long.Parse(x.Trim()))
                            .ToList();

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        foreach (var anId in theIds)
                        {
                            state.Tickets.Remove(anId);
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    })
                    .MapDelete("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Tickets.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    });
            }
        }

        private static void HandleTicketComment(TicketComment comment, State state, long ticketId)
        {
            if (comment == null) return;

            comment.Id = long.Parse(Rand.Next().ToString());

            if (state.TicketComments.ContainsKey(ticketId))
            {
                state.TicketComments[ticketId].Add(comment);
            }
            else
            {
                state.TicketComments.Add(ticketId, new List<TicketComment>
                {
                    comment
                });
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
                    services.AddSingleton(_ => new State());
                    services.AddSingleton(_ => new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<TicketCreateRequest, TicketResponse>()
                                .ForMember(r => r.Ticket, r => r.MapFrom(req => req));
                            cfg.CreateMap<TicketUpdateRequest, TicketResponse>()
                                .ForMember(r => r.Ticket, r => r.MapFrom(req => req));
                            cfg.CreateMap<TicketCreateRequest, Ticket>();
                            cfg.CreateMap<TicketUpdateRequest, Ticket>();
                        }).CreateMapper());
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

        public sealed override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : "";
        }

        public Uri BaseUri => Client.BaseAddress;

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
