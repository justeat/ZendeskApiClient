using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestsResource : AbstractBaseResource<RequestsResource>,
        IRequestsResource
    {
        private const string ResourceUri = "api/v2/requests";
        private const string CommentsResourceUri = "api/v2/requests/{0}/comments";

        public RequestsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "requests")
        { }
        
        public async Task<IPagination<Request>> GetAllAsync(PagerParameters pager = null)
        {
            return await GetAsync<RequestsResponse>(
                ResourceUri,
                "list-requests",
                "GetAllAsync",
                pager);
        }

        public async Task<Request> GetAsync(long requestId)
        {
            var response = await GetWithNotFoundCheckAsync<RequestResponse>(
                $"{ResourceUri}/{requestId}",
                "show-request",
                $"GetAsync({requestId})",
                $"Request {requestId} not found");

            return response?
                .Request;
        }

        //TODO: FIx
        public async Task<IPagination<Request>> SearchAsync(IZendeskQuery query, PagerParameters pager = null)
        {
            return await GetAsync<RequestsResponse>(
                $"{ResourceUri}/search?" /*+ query.WithTypeFilter<Request>().BuildQuery()*/,
                "search-requests",
                "SearchAsync",
                pager);
        }

        public async Task<IPagination<TicketComment>> GetAllComments(long requestId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<TicketCommentsResponse>(
                string.Format(CommentsResourceUri, requestId),
                "getting-comments",
                $"GetAllComments({requestId})",
                $"Could not find any comments for request {requestId} as request was not found",
                pager);
        }

        public async Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId)
        {
            return await GetWithNotFoundCheckAsync<TicketComment>(
                $"{string.Format(CommentsResourceUri, requestId)}/{commentId}",
                "getting-comments",
                $"GetAllComments({requestId})",
                $"Could not find any comment for request {requestId} and comment {commentId} was not found");
        }

        public async Task<Request> CreateAsync(Request request)
        {
            var response = await CreateAsync<RequestResponse, RequestCreateRequest>(
                ResourceUri,
                new RequestCreateRequest(request),
                "create-request"
            );

            return response?
                .Request;
        }

        public async Task<Request> UpdateAsync(Request request)
        {
            var response = await UpdateWithNotFoundCheckAsync<RequestResponse, RequestUpdateRequest>(
                $"{ResourceUri}/{request.Id}",
                new RequestUpdateRequest(request),
                "update-request",
                $"Cannot update request as request {request.Id} cannot be found");

            return response?
                .Request;
        }
    }
}
