using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketState : State<Ticket>
    {
        public readonly IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
    }

    internal class DeletedTicketResourceSampleSite : SampleSite<TicketState, Ticket>
    {
        public DeletedTicketResourceSampleSite(string resource, Dictionary<long, Ticket> ticketState)
            : base(
                resource,
                MatchesRequest,
                ConfigureWebHost,
                state => state.Items = ticketState)
        { }

        private static void ConfigureWebHost(WebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
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

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_tickets.json", (req, resp, routeData) =>
                    {
                        var pager = new Pager(int.Parse(req.Query["page"]), int.Parse(req.Query["per_page"]), 100);
                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        var tickets = state
                            .Items
                            .Values
                            .Skip(pager.GetStartIndex())
                            .Take(pager.PageSize);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new DeletedTicketsListResponse { Tickets = tickets});
                    })
                    .MapPut("api/v2/deleted_tickets/{id}/restore.json", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsJson(new object());
                        }

                        state.Items.Remove(id);
                        
                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    })
                    .MapPut("api/v2/deleted_tickets/restore_many", (req, resp, routeData) =>
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        foreach (var anId in theIds)
                        {
                            state.Items.Remove(anId);
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    })
                    .MapDelete("api/v2/deleted_tickets/destroy_many", (req, resp, routeData) =>
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        foreach (var anId in theIds)
                        {
                            state.Items.Remove(anId);
                        }
                        
                        var jobStatusResponse = new JobStatusResponse
                        {
                            Id = Rand.Next(999).ToString(),
                            Results = null,
                            Total = 0,
                            Message = null,
                            Progress = 0,
                            Status = "queued"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleJobStatusResponse {JobStatus = jobStatusResponse});
                    })
                    .MapDelete("api/v2/deleted_tickets/{id}.json", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<TicketState>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsync("Not found");
                        }

                        state.Items.Remove(id);

                        var jobStatusResponse = new JobStatusResponse
                        {
                            Id = Rand.Next(999).ToString(),
                            Results = null,
                            Total = 0,
                            Message = null,
                            Progress = 0,
                            Status = "queued"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleJobStatusResponse {JobStatus = jobStatusResponse});
                    });
            }
        }
    }
}
