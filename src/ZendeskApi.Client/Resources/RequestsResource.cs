using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestsResource : IRequestsResource
    {
        private const string ResourceUri = "api/v2/requests";
        private const string CommentsResourceUri = "api/v2/requests/{0}/comments";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>("RequestsResource: {0}");

        public RequestsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<RequestsResponse>()).Item;
            }
        }

        public async Task<Request> GetAsync(long requestId)
        {
            using (_loggerScope(_logger, $"GetAsync({requestId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(requestId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Request {0} not found", requestId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Request>> SearchAsync(IZendeskQuery<Request> query)
        {
            using (_loggerScope(_logger, $"SearchAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync("search?" + query.BuildQuery()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<RequestsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<TicketComment>> GetAllComments(long requestId)
        {
            using (_loggerScope(_logger, $"GetAllComments({requestId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(CommentsResourceUri, requestId)).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Could not find any comments for request {0} as request was not found", requestId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketCommentsResponse>()).Item;
            }
        }

        public async Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId)
        {
            using (_loggerScope(_logger, $"GetAsync({requestId},{commentId})"))
            using (var client = _apiClient.CreateClient(string.Format(CommentsResourceUri, requestId)))
            {
                var response = await client.GetAsync(commentId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Could not find any comment for request {0} and comment {1} was not found", requestId, commentId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketCommentResponse>()).Item;
            }
        }

        public async Task<Request> PostAsync(Request request)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new RequestRequest { Item = request }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/requests#create-request");
                }

                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }

        public async Task<Request> PutAsync(Request request)
        {
            using (_loggerScope(_logger, "PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(request.Id.ToString(), new RequestRequest { Item = request }).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update request as request {0} cannot be found", request.Id);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }
    }
}
