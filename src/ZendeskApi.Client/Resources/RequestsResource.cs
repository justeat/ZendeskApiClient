using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
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
            LoggerMessage.DefineScope<string>(typeof(RequestsResource).Name + ": {0}");

        public RequestsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }
        
        public async Task<IPagination<Request>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<RequestsResponse>();
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

                return (await response
                    .Content
                    .ReadAsAsync<RequestResponse>())
                    .Request;
            }
        }

        //TODO: FIx
        public async Task<IPagination<Request>> SearchAsync(IZendeskQuery query, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"SearchAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync("search?" /*+ query.WithTypeFilter<Request>().BuildQuery()*/, pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<RequestsResponse>();
            }
        }

        public async Task<IPagination<TicketComment>> GetAllComments(long requestId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllComments({requestId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(CommentsResourceUri, requestId), pager).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Could not find any comments for request {0} as request was not found", requestId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<TicketCommentsResponse>();
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

                return await response.Content.ReadAsAsync<TicketComment>();
            }
        }

        public async Task<Request> CreateAsync(Request request)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new RequestCreateRequest(request)).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/requests#create-request");
                }

                return (await response
                        .Content
                        .ReadAsAsync<RequestResponse>())
                    .Request;
            }
        }

        public async Task<Request> UpdateAsync(Request request)
        {
            using (_loggerScope(_logger, "PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(request.Id.ToString(), new RequestUpdateRequest(request)).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update request as request {0} cannot be found", request.Id);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response
                        .Content
                        .ReadAsAsync<RequestResponse>())
                    .Request;
            }
        }
    }
}
