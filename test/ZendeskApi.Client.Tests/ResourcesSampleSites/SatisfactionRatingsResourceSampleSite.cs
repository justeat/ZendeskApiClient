using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class SatisfactionRatingsState : State<SatisfactionRating>
    {
        public IDictionary<long, List<SatisfactionRating>> SatisfactionRatingsByTicket = new Dictionary<long, List<SatisfactionRating>>();
    }

    internal class SatisfactionRatingsResourceSampleSite : SampleSite<SatisfactionRatingsState, SatisfactionRating>
    {
        public SatisfactionRatingsResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/satisfaction_ratings/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<SatisfactionRatingsState>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var sr = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SatisfactionRatingResponse { SatisfactionRating = sr });
                    })
                    .MapGet("api/v2/satisfaction_ratings", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<SatisfactionRatingsState>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SatisfactionRatingsResponse { SatisfactionRatings = state.Items.Values });
                    })
                    .MapPost("api/v2/tickets/{ticketId}/satisfaction_rating", (req, resp, routeData) =>
                    {
                        var sr = req.Body.ReadAs<SatisfactionRating>();
                        var ticketId = long.Parse(routeData.Values["ticketId"].ToString());
                        var state = req.HttpContext.RequestServices.GetRequiredService<SatisfactionRatingsState>();

                        sr.Id = long.Parse(Rand.Next().ToString());
                        state.Items.Add(sr.Id.Value, sr);

                        if (state.SatisfactionRatingsByTicket.ContainsKey(ticketId))
                        {
                            state.SatisfactionRatingsByTicket[ticketId].Add(sr);
                        }
                        else {
                            state.SatisfactionRatingsByTicket.Add(ticketId, new List<SatisfactionRating> { sr });
                        }

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(sr);
                    })
                    ;
            }
        }
    }
}
