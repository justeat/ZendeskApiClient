using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UserIdentitiesResourceSampleSite : SampleSite<State<UserIdentity>, UserIdentity>
    {
        public UserIdentitiesResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<UserIdentity> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserIdentity
                {
                    Id = i,
                    UserId = i,
                    Name = $"name.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/{userId}/identities/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<UserIdentityResponse<UserIdentity>, UserIdentity>(
                            req,
                            resp,
                            routeData,
                            item => new UserIdentityResponse<UserIdentity> { Identity = item });
                    })
                    .MapGet("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<UserIdentitiesResponse, UserIdentity>(
                            req,
                            resp,
                            routeData.Values["userId"].ToString(),
                            (id, items) => items.Where(x => x.UserId == id),
                            items => new UserIdentitiesResponse
                            {
                                Identities = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/users/{userId}/identities", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<UserIdentityRequest<UserIdentity>>();
                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            request.Identity,
                            item => new UserIdentityResponse<UserIdentity> { Identity = item });
                    })
                    .MapPost("api/v2/end_users/{userId}/identities", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<UserIdentityRequest<UserIdentity>>();
                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            request.Identity,
                            item => new UserIdentityResponse<UserIdentity> { Identity = item });
                    })
                    .MapPut("api/v2/users/{userId}/identities/{id}", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<UserIdentityRequest<UserIdentity>>();
                        await RequestHelper.Update(
                            req,
                            resp,
                            routeData,
                            request.Identity,
                            item => new UserIdentityResponse<UserIdentity> { Identity = item });
                    })
                    .MapDelete("api/v2/users/{userid}/identities/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<UserIdentity>(
                            req,
                            resp,
                            routeData);
                    })
                    ;
            }
        }
    }
}
