using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
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

        //public async Task<Request> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        //{
        //    using (var client = _apiClient.CreateClient())
        //    {
        //        // TODO: ngm make nicer
        //        var query = $"status={string.Join(",", requestedStatuses).ToLower()}";
        //        var response = await client.GetAsync($"{ResourceUri}?{query}").ConfigureAwait(false);
        //        return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
        //    }
        //}

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
    }
}
