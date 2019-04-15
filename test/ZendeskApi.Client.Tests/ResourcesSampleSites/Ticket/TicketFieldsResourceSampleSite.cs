using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketFieldsResourceSampleSite : SampleSite<TicketField>
    {
        public TicketFieldsResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketField>>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var ticketField = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketFieldResponse { TicketField = ticketField });
                    })
                    .MapGet("api/v2/ticket_fields", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketField>>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketFieldsResponse { TicketFields = state.Items.Values });
                    })
                    .MapPost("api/v2/ticket_fields", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.ReadAs<TicketFieldCreateUpdateRequest>();
                        Assert.NotNull(ticket);
                        var ticketField = ticket.TicketField;

                        if (ticketField.Tag != null && ticketField.Tag.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketField>>();

                        ticketField.Id = long.Parse(Rand.Next().ToString());
                        state.Items.Add(ticketField.Id.Value, ticketField);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(ticket);
                    })
                    .MapPut("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var ticket = req.Body.ReadAs<TicketFieldCreateUpdateRequest>();
                        Assert.NotNull(ticket);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketField>>();

                        state.Items[id] = ticket.TicketField;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketFieldResponse{TicketField = state.Items[id]});
                    })
                    .MapDelete("api/v2/ticket_fields/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketField>>();

                        state.Items.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    })
                    ;
            }
        }
    }
}
