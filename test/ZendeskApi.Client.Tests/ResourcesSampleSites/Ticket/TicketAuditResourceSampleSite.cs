using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class TicketAuditResourceSampleSite : SampleSite<TicketAudit>
    {
        public TicketAuditResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<TicketAudit> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new TicketAudit
                {
                    Id = i,
                    TicketId = i
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/ticket_audits", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<TicketAuditResponse, TicketAudit>(
                            req,
                            resp,
                            items => new TicketAuditResponse { Audits = items });
                    })
                    .MapGet("api/v2/tickets/{ticketId}/audits", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["ticketId"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketAudit>>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var obj = state.Items
                            .Where(x => x.Value.TicketId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketAuditResponse { Audits = obj });
                    })
                    .MapGet("api/v2/tickets/{ticketId}/{auditId}", (req, resp, routeData) => {

                        var id = long.Parse(routeData.Values["auditId"].ToString());
                        var ticketId = long.Parse(routeData.Values["ticketId"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<TicketAudit>>();

                        if (ticketId == int.MinValue || id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var obj = state.Items
                            .Where(x => x.Value.TicketId == ticketId && x.Value.Id == id)
                            .Select(p => p.Value).ToList();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new TicketAuditResponse { Audits = obj });
                    });
            }
        }
    }
}