using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class OrganizationFieldsResourceSampleSite : SampleSite<OrganizationField>
    {
        public OrganizationFieldsResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<OrganizationField> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new OrganizationField
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
                    .MapGet("api/v2/organization_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<OrganizationFieldResponse, OrganizationField>(
                            req,
                            resp,
                            routeData,
                            item => new OrganizationFieldResponse
                            {
                                OrganizationField = item
                            });
                    })
                    .MapGet("api/v2/organization_fields", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<OrganizationFieldsResponse, OrganizationField>(
                            req,
                            resp,
                            items => new OrganizationFieldsResponse
                            {
                                OrganizationFields = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/organization_fields", (req, resp, routeData) =>
                    {
                        var request = req.Body.ReadAs<OrganizationFieldCreateUpdateRequest>();
                        var field = request.OrganizationField;

                        return RequestHelper.Create<OrganizationFieldResponse, OrganizationField>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            field,
                            item => new OrganizationFieldResponse
                            {
                                OrganizationField = item
                            });
                    })
                    .MapPut("api/v2/organization_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Update<OrganizationFieldResponse, OrganizationField>(
                            req,
                            resp,
                            routeData,
                            req.Body
                                .ReadAs<OrganizationFieldCreateUpdateRequest>()
                                .OrganizationField,
                            item => new OrganizationFieldResponse
                            {
                                OrganizationField = item
                            });
                    })
                    .MapDelete("api/v2/organization_fields/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<OrganizationField>(
                            req,
                            resp,
                            routeData);
                    })
                    ;
            }
        }
    }
}