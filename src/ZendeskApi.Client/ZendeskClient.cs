using System;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public class ZendeskClient : IZendeskClient
    {
        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        public ZendeskClient(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        private Lazy<ITicketsResource> TicketsLazy => new Lazy<ITicketsResource>(() => new TicketsResource(_apiClient, _logger));
        public ITicketsResource Tickets => TicketsLazy.Value;

        private Lazy<ITicketCommentResource> TicketCommentsLazy => new Lazy<ITicketCommentResource>(() => new TicketCommentResource(_apiClient));
        public ITicketCommentResource TicketComments => TicketCommentsLazy.Value;

        private Lazy<IRequestCommentResource> RequestCommentsLazy => new Lazy<IRequestCommentResource>(() => new RequestCommentResource(_apiClient));
        public IRequestCommentResource RequestComments => RequestCommentsLazy.Value;

        private Lazy<IOrganizationResource> OrganizationsLazy => new Lazy<IOrganizationResource>(() => new OrganizationResource(_apiClient));
        public IOrganizationResource Organizations => OrganizationsLazy.Value;

        private Lazy<ISearchResource> SearchLazy => new Lazy<ISearchResource>(() => new SearchResource(_apiClient, _logger));
        public ISearchResource Search => SearchLazy.Value;

        private Lazy<IGroupsResource> GroupsLazy => new Lazy<IGroupsResource>(() => new GroupsResource(_apiClient, _logger));
        public IGroupsResource Groups => GroupsLazy.Value;

        private Lazy<IUsersResource> UsersLazy => new Lazy<IUsersResource>(() => new UsersResource(_apiClient, _logger));
        public IUsersResource Users => UsersLazy.Value;

        private Lazy<IUserIdentityResource> UserIdentitiesLazy => new Lazy<IUserIdentityResource>(() => new UserIdentityResource(_apiClient));
        public IUserIdentityResource UserIdentities => UserIdentitiesLazy.Value;

        private Lazy<IAttachmentsResource> AttachmentsLazy => new Lazy<IAttachmentsResource>(() => new AttachmentsResource(_apiClient, _logger));
        public IAttachmentsResource Attachments => AttachmentsLazy.Value;

        private Lazy<ITicketFieldsResource> TicketFieldsLazy => new Lazy<ITicketFieldsResource>(() => new TicketFieldsResource(_apiClient, _logger));
        public ITicketFieldsResource TicketFields => TicketFieldsLazy.Value;

        private Lazy<ITicketFormResource> TicketFormsLazy => new Lazy<ITicketFormResource>(() => new TicketFormResource(_apiClient));
        public ITicketFormResource TicketForms => TicketFormsLazy.Value;

        private Lazy<IOrganizationMembershipsResource> OrganizationMembershipsLazy => new Lazy<IOrganizationMembershipsResource>(() => new OrganizationMembershipsResource(_apiClient, _logger));
        public IOrganizationMembershipsResource OrganizationMemberships => OrganizationMembershipsLazy.Value;

        private Lazy<IRequestResource> RequestLazy => new Lazy<IRequestResource>(() => new RequestResource(_apiClient));
        public IRequestResource Request => RequestLazy.Value;

        private Lazy<ISatisfactionRatingResource> SatisfactionRatingLazy => new Lazy<ISatisfactionRatingResource>(() => new SatisfactionRatingResource(_apiClient));
        public ISatisfactionRatingResource SatisfactionRating => SatisfactionRatingLazy.Value;
    }
}
