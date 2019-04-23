using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketResourceState : State<Ticket>
    {
        public readonly IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
    }

    internal class TicketResourceSampleSite : SampleSite<TicketResourceState, Ticket>
    {
        public TicketResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                ConfigureWebHost,
                PopulateState)
        { }

        private static void ConfigureWebHost(WebHostBuilder builder)
        {
            builder
                .ConfigureServices(services => {
                    services.AddSingleton(_ => new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TicketCreateRequest, TicketResponse>()
                            .ForMember(r => r.Ticket, r => r.MapFrom(req => req));
                        cfg.CreateMap<TicketUpdateRequest, TicketResponse>()
                            .ForMember(r => r.Ticket, r => r.MapFrom(req => req));
                        cfg.CreateMap<TicketCreateRequest, Ticket>();
                        cfg.CreateMap<TicketUpdateRequest, Ticket>();
                    }).CreateMapper());
                });
        }

        private static void PopulateState(TicketResourceState state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Ticket
                {
                    Id = i,
                    Subject = $"My printer is on fire! {i}",
                    ExternalId = i.ToString(),
                    OrganisationId = i,
                    RequesterId = i,
                    AssigneeId = i,
                    CollaboratorIds = new List<long> { i }
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/tickets/show_many", (req, resp, routeData) =>
                    {
                        return RequestHelper.Many<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            ticket => ticket.Id,
                            ticket => ticket.ExternalId,
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<TicketResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData,
                            item => new TicketResponse
                            {
                                Ticket = item
                            });
                    })
                    .MapGet("api/v2/tickets", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/tickets/{id}/comments", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketResourceState>();

                        var comments = state.TicketComments.ContainsKey(id) ? state.TicketComments[id] : new List<TicketComment>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketCommentsResponse { Comments = comments });
                    })
                    .MapGet("api/v2/users/{id}/tickets/assigned", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData.Values["id"].ToString(),
                            (id, items) => items.Where(x => x.AssigneeId.HasValue && x.AssigneeId == id),
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/users/{id}/tickets/ccd", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData.Values["id"].ToString(),
                            (id, items) => items.Where(x => x.CollaboratorIds != null && x.CollaboratorIds.Contains(id)),
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/users/{id}/tickets/requested", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData.Values["id"].ToString(),
                            (id, items) => items.Where(x => x.RequesterId.HasValue && x.RequesterId == id),
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/organizations/{id}/tickets", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<TicketsListResponse, Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData.Values["id"].ToString(),
                            (id, items) => items.Where(x => x.OrganisationId == id),
                            items => new TicketsListResponse
                            {
                                Tickets = items,
                                Count = items.Count
                            });
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketResourceState>();
                        ticketResponse.Ticket.Id = long.Parse(Rand.Next().ToString());
                        ticketResponse.Ticket.Url = new Uri($"https://company.zendesk.com/api/v2/tickets/{ticketResponse.Ticket.Id}.json");


                        HandleTicketComment(ticket.Comment, state, ticketResponse.Ticket.Id);

                        state.Items.Add(ticketResponse.Ticket.Id, ticketResponse.Ticket);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(ticketResponse);
                    })
                    .MapPut("api/v2/tickets/update_many.json", (req, resp, routeData) =>
                    {
                        var theIds = req.Query["ids"]
                            .SelectMany(q => q.Split(','))
                            .Select(long.Parse);

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketResourceState>();

                        var ticketRequestWrapper = req.Body.ReadAs<TicketListRequest<TicketUpdateRequest>>();

                        var ticketsById = ticketRequestWrapper.Tickets.ToDictionary(t => t.Id, t => t);

                        if (ticketRequestWrapper.Tickets.Any(t => t.Id == int.MinValue))
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

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

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketResourceState>();

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

                        var ticketRequestWrapper = req.Body.ReadAs<TicketRequest<TicketUpdateRequest>>();
                        var ticketRequest = ticketRequestWrapper.Ticket;

                        HandleTicketComment(ticketRequest.Comment, state, ticketRequest.Id);

                        var ticketResponse = new TicketResponse {Ticket = state.Items[id] };
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        mapper.Map(ticketRequest, ticketResponse);

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(ticketResponse);
                    })
                    .MapDelete("api/v2/tickets/destroy_many.json", (req, resp, routeData) =>
                    {
                        return RequestHelper.DeleteMany<Ticket, TicketResourceState>(
                            req,
                            resp);
                    })
                    .MapDelete("api/v2/tickets/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Ticket, TicketResourceState>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }

        private static void HandleTicketComment(TicketComment comment, TicketResourceState state, long ticketId)
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
    }
}
