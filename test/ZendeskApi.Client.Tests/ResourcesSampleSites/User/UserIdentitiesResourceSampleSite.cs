using System;
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
                        return RequestHelper.GetById<UserIdentity, UserIdentity>(
                            req,
                            resp,
                            routeData,
                            item => item);
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
                    .MapPost("api/v2/users/{userId}/identities", (req, resp, routeData) =>
                    {
                        return RequestHelper.Create<UserIdentity, UserIdentity>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            req.Body.ReadAs<UserIdentity>(),
                            item => item);
                    })
                    .MapPost("api/v2/end_users/{userId}/identities", (req, resp, routeData) =>
                    {
                        return RequestHelper.Create<UserIdentity, UserIdentity>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            req.Body.ReadAs<UserIdentity>(),
                            item => item);
                    })
                    .MapPut("api/v2/users/{id}/identities", (req, resp, routeData) =>
                    {
                        return RequestHelper.Update<UserIdentity, UserIdentity>(
                            req,
                            resp,
                            routeData,
                            req.Body.ReadAs<UserIdentity>(),
                            item => item);
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
