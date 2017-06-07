using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskSampleSite : IDisposable
    {
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapPost("api/v2/tickets", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.Deserialize<TicketRequest>().Item;

                        if (ticket.Tags != null && ticket.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        ticket.Id = long.Parse(new Random().Next().ToString());

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = ticket }));

                        return Task.CompletedTask;
                    })
                    .MapPost("api/v2/tickets/create_many", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.Deserialize<TicketsRequest>().Item;

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        
                        resp.WriteAsync(JsonConvert.SerializeObject(new JobStatusResponse { Item = new JobStatus { Id = "sadasdsadsa" }  }));

                        return Task.CompletedTask;
                    })
                    .MapPut("api/v2/tickets/491", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.Deserialize<TicketRequest>().Item;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = ticket }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/tickets/435", (req, resp, routeData) =>
                    {
                        var ticket = new Ticket
                        {
                            Id = 435L,
                            Subject = "My printer is on fire!",
                            Comment = new TicketComment
                            {
                                Body = "The smoke is very colorful."
                            }
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new TicketResponse { Item = ticket }));

                        return Task.CompletedTask;
                    });
            }
        }

        private readonly TestServer _server;

        public HttpClient Client { get; }

        public ZendeskSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => services.AddRouting())
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);
            Client = _server.CreateClient();

            resource = resource?.Trim('/');

            if (resource != null)
            {
                resource = resource + "/";
            }

            Client.BaseAddress = new Uri($"http://localhost/{resource}");
        }

        public Uri BaseUri
        {
            get { return Client.BaseAddress; }
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
