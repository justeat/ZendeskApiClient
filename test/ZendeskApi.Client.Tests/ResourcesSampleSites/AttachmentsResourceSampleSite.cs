using System;
using System.Diagnostics;
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
    internal class AttachmentsResourceSampleSite : SampleSite<State<Attachment>, Attachment>
    {
        public AttachmentsResourceSampleSite(string resource)
        : base(
            resource, 
            MatchesRequest,
            null,
            PopulateState)
        { }

        private static void PopulateState(State<Attachment> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Attachment
                {
                    Id = i,
                    FileName = $"filename.{i}"
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/attachments/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<Attachment, Attachment>(
                            req,
                            resp,
                            routeData,
                            item => item);
                    })
                    .MapPost("api/v2/uploads", (req, resp, routeData) =>
                    {
                        var filename = req.Query["filename"];

                        if (string.IsNullOrWhiteSpace(filename))
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        resp.Headers.Add("Location", "https://localhost/api/v2/attachments/498483");

                        var state = req
                            .HttpContext
                            .RequestServices
                            .GetRequiredService<State<Attachment>>();

                        var attachment = new Attachment
                        {
                            ContentType = "text/plain",
                            ContentUrl = "https://company.zendesk.com/attachments/crash.log",
                            Size = req.Body.Length,
                            FileName = req.Query["filename"],
                            Id = long.Parse(Rand.Next().ToString())
                        };

                        state.Items.Add(attachment.Id.Value, attachment);

                        resp.WriteAsJson(new UploadResponse{
                            Upload = new Upload {
                                Attachment = attachment,
                                Token = "6bk3gql82em5nmf"
                            }
                        });

                        return Task.CompletedTask;
                    })
                    .MapDelete("api/v2/uploads/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Attachment>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
