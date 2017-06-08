using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests
{
    public class AttachmentsResourceSampleSite : SampleSite
    {
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapPost("api/v2/uploads", (req, resp, routeData) =>
                    {
                        Debug.Assert(req.Query["filename"] == "crash.log");
                        Debug.Assert(req.Query["token"] == "6bk3gql82em5nmf");
                        Debug.Assert(req.ContentType == "application/binary");

                        var strings = new List<string>();
                        using (var inputStreamReader = new StreamReader(req.Body))
                        {
                            string line;
                            while ((line = inputStreamReader.ReadLine()) != null)
                            {
                                strings.Add(line);
                            }
                        }
                        
                        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(strings[1].Replace("Content-Disposition: ", ""));

                        resp.StatusCode = (int)HttpStatusCode.Created;

                        resp.Headers.Add("Location", "https://localhost/api/v2/attachments/498483");

                        resp.WriteAsync(JsonConvert.SerializeObject(new UploadResponse { Item = new Upload {
                            Attachment = new Attachment {
                                ContentType = "text/plain",
                                ContentUrl = "https://company.zendesk.com/attachments/crash.log",
                                Size = strings[3].Length,
                                FileName = parsedContentDisposition.FileName,
                                Id = long.Parse(RAND.Next().ToString())
                            },
                            Token = "6bk3gql82em5nmf"
                        } }));

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
                .ConfigureServices(services => services.AddRouting())
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
