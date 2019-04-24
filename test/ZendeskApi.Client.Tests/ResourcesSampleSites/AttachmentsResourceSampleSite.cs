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
        : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/attachments/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Attachment>>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var obj = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(obj);
                    })
                    .MapPost("api/v2/uploads", (req, resp, routeData) =>
                    {
                        Debug.Assert(req.Query["filename"] == "crash.log");
                        Debug.Assert(req.Query["token"] == "6bk3gql82em5nmf");
                        Debug.Assert(req.ContentType == "application/binary");

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        resp.Headers.Add("Location", "https://localhost/api/v2/attachments/498483");

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Attachment>>();

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
                    });
            }
        }
    }
}
