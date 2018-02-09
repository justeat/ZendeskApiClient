using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class AttachmentsResourceSampleSite : SampleSite
    {
        private class State
        {
            public readonly IDictionary<long, Attachment> Attachments = new Dictionary<long, Attachment>();
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/attachments/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Attachments.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var obj = state.Attachments.Single(x => x.Key == id).Value;

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

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        
                        var attachment = new Attachment
                        {
                            ContentType = "text/plain",
                            ContentUrl = "https://company.zendesk.com/attachments/crash.log",
                            Size = req.Body.Length,
                            FileName = req.Query["filename"],
                            Id = long.Parse(Rand.Next().ToString())
                        };

                        state.Attachments.Add(attachment.Id.Value, attachment);

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

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public AttachmentsResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => {
                    services.AddSingleton<State>((_) => new State());
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);

            RefreshClient(resource);
        }

        public override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : resource;
        }

        public Uri BaseUri
        {
            get { return Client.BaseAddress; }
        }

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
