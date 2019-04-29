using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class JobStatusSampleSite : SampleSite<State<JobStatusResponse>, JobStatusResponse>
    {
        public JobStatusSampleSite(string resource)
        : base(
            resource,
            MatchesRequest,
            null,
            PopulateState)
        { }

        private static void PopulateState(State<JobStatusResponse> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new JobStatusResponse
                {
                    Id = i.ToString(),
                    Status = $"status.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/job_statuses", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<JobStatusListResponse, JobStatusResponse>(
                            req,
                            resp,
                            items => new JobStatusListResponse
                            {
                                JobStatuses = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/job_statuses/show_many", (req, resp, routeData) =>
                    {
                        return RequestHelper.Many<JobStatusListResponse, JobStatusResponse>(
                            req,
                            resp,
                            item => long.Parse(item.Id),
                            item => string.Empty,
                            items => new JobStatusListResponse
                            {
                                JobStatuses = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/job_statuses/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<SingleJobStatusResponse, JobStatusResponse>(
                            req,
                            resp,
                            routeData,
                            item => new SingleJobStatusResponse
                            {
                                JobStatus = item
                            });
                    });
            }
        }
    }
}
