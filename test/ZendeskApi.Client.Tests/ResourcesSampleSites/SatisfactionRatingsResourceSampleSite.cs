using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class SatisfactionRatingsResourceSampleSite : SampleSite<State<SatisfactionRating>, SatisfactionRating>
    {
        public SatisfactionRatingsResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<SatisfactionRating> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new SatisfactionRating
                {
                    Id = i,
                    Comment = $"comment.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/satisfaction_ratings/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<SatisfactionRatingResponse, SatisfactionRating>(
                            req,
                            resp,
                            routeData,
                            item => new SatisfactionRatingResponse
                            {
                                SatisfactionRating = item
                            });
                    })
                    .MapGet("api/v2/satisfaction_ratings", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<SatisfactionRatingsResponse, SatisfactionRating>(
                            req,
                            resp,
                            items => new SatisfactionRatingsResponse
                            {
                                SatisfactionRatings = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/tickets/{ticketId}/satisfaction_rating", (req, resp, routeData) =>
                    {
                        return RequestHelper.Create<SatisfactionRating, SatisfactionRating>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            req.Body.ReadAs<SatisfactionRating>(),
                            item => item);
                    })
                    ;
            }
        }
    }
}
