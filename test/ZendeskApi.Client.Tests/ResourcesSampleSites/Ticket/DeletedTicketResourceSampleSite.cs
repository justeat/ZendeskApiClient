using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class DeletedTicketResourceSampleSite : SampleSite
    {
        public class InternalState {
            public IDictionary<long, Ticket> Tickets = new Dictionary<long, Ticket>();
            public readonly IDictionary<long, IList<TicketComment>> TicketComments = new Dictionary<long, IList<TicketComment>>();
        }
         
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_tickets.json", (req, resp, routeData) =>
                    {
                        var pager = new Pager(int.Parse(req.Query["page"]), int.Parse(req.Query["per_page"]), 100);
                        var state = req.HttpContext.RequestServices.GetRequiredService<InternalState>();

                        var tickets = state
                            .Tickets
                            .Values
                            .Skip(pager.GetStartIndex())
                            .Take(pager.PageSize);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new DeletedTicketsListResponse { Tickets = tickets});
                    })
                    .MapPut("api/v2/deleted_tickets/{id}/restore.json", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<InternalState>();

                        if (!state.Tickets.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsJson(new object());
                        }

                        state.Tickets.Remove(id);
                        
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<InternalState>();

                        foreach (var anId in theIds)
                        {
                            state.Tickets.Remove(anId);
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<InternalState>();

                        foreach (var anId in theIds)
                        {
                            state.Tickets.Remove(anId);
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<InternalState>();

                        if (!state.Tickets.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return resp.WriteAsync("Not found");
                        }

                        state.Tickets.Remove(id);

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
        
        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public InternalState State => new InternalState();
        
        public DeletedTicketResourceSampleSite(string resource, Dictionary<long, Ticket> ticketState)
        { 
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => {
                    services.AddSingleton(_ => new InternalState
                    {
                        Tickets = ticketState
                    });
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
        
        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
