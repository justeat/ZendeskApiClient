using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests.User;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UserFieldsResourceSampleSite : SampleSite<UserField>
    {
        public UserFieldsResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<UserField> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserField
                {
                    Id = i,
                    RawTitle = $"raw.title.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<UserFieldResponse, UserField>(
                            req,
                            resp,
                            routeData,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapGet("api/v2/user_fields", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<UserFieldsResponse, UserField>(
                            req,
                            resp,
                            items => new UserFieldsResponse
                            {
                                UserFields = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/user_fields", (req, resp, routeData) =>
                    {
                        var request = req.Body.ReadAs<UserFieldCreateUpdateRequest>();
                        var field = request.UserField;

                        return RequestHelper.Create<UserFieldResponse, UserField>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            field,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapPut("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Update<UserFieldResponse, UserField>(
                            req,
                            resp,
                            routeData,
                            req.Body
                                .ReadAs<UserFieldCreateUpdateRequest>()
                                .UserField,
                            item => new UserFieldResponse
                            {
                                UserField = item
                            });
                    })
                    .MapDelete("api/v2/user_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<UserField>(
                            req,
                            resp,
                            routeData);
                    })
                    ;
            }
        }
    }
}
