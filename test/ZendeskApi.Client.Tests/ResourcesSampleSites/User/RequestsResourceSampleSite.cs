using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class RequestsResourceSampleSite : SampleSite<Request>
    {
        public RequestsResourceSampleSite(string resource)
            : base(
                resource,
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<Request> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Request
                {
                    Id = i,
                    Subject = $"subject.{i}",
                    Description = $"description.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                        .MapGet("api/v2/requests/{id}", (req, resp, routeData) =>
                        {
                            return RequestHelper.GetById<RequestResponse, Request>(
                                req,
                                resp,
                                routeData,
                                item => new RequestResponse
                                {
                                    Request = item
                                });
                        })
                        .MapGet("api/v2/requests", (req, resp, routeData) =>
                        {
                            return RequestHelper.List<RequestsResponse, Request>(
                                req,
                                resp,
                                items => new RequestsResponse
                                {
                                    Requests = items,
                                    Count = items.Count
                                });
                        })
                        .MapGet("api/v2/requests/{id}/comments/{commentId}", (req, resp, routeData) =>
                        {
                            return RequestHelper.GetById<TicketComment, Request>(
                                req,
                                resp,
                                routeData,
                                item => new TicketComment
                                {
                                    Id = item.Id,
                                    Body = $"body.{item.Id}"
                                });
                        })
                        .MapGet("api/v2/requests/{id}/comments", (req, resp, routeData) =>
                        {
                            return RequestHelper.List<TicketCommentsResponse, Request>(
                                req,
                                resp,
                                items => new TicketCommentsResponse
                                {
                                    Comments = items.Select(i => new TicketComment
                                    {
                                        Id = i.Id,
                                        Body = $"body.{i.Id}"
                                    }),
                                    Count = items.Count
                                });
                        })
                        .MapPost("api/v2/requests", (req, resp, routeData) =>
                        {
                            return RequestHelper.Create<RequestResponse, Request>(
                                req,
                                resp,
                                routeData,
                                item => item.Id,
                                req.Body
                                    .ReadAs<RequestCreateRequest>()
                                    .Request,
                                item => new RequestResponse
                                {
                                    Request = item
                                });
                        })
                        .MapPut("api/v2/requests/{id}", (req, resp, routeData) =>
                        {
                            return RequestHelper.Update<RequestResponse, Request>(
                                req,
                                resp,
                                routeData,
                                req.Body
                                    .ReadAs<RequestUpdateRequest>()
                                    .Request,
                                item => new RequestResponse
                                {
                                    Request = item
                                });
                        })
                    ;
            }
        }
    }
}