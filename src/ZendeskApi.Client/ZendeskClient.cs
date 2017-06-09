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

        private Lazy<ITicketCommentsResource> TicketCommentsLazy => new Lazy<ITicketCommentsResource>(() => new TicketCommentsResource(_apiClient, _logger));
        public ITicketCommentsResource TicketComments => TicketCommentsLazy.Value;

        private Lazy<IOrganizationResource> OrganizationsLazy => new Lazy<IOrganizationResource>(() => new OrganizationResource(_apiClient));
        public IOrganizationResource Organizations => OrganizationsLazy.Value;

        private Lazy<ISearchResource> SearchLazy => new Lazy<ISearchResource>(() => new SearchResource(_apiClient, _logger));
        public ISearchResource Search => SearchLazy.Value;

        private Lazy<IGroupsResource> GroupsLazy => new Lazy<IGroupsResource>(() => new GroupsResource(_apiClient, _logger));
        public IGroupsResource Groups => GroupsLazy.Value;

        private Lazy<IUsersResource> UsersLazy => new Lazy<IUsersResource>(() => new UsersResource(_apiClient, _logger));
        public IUsersResource Users => UsersLazy.Value;

        private Lazy<IUserIdentityResource> UserIdentitiesLazy => new Lazy<IUserIdentityResource>(() => new UserIdentitiesResource(_apiClient, _logger));
        public IUserIdentityResource UserIdentities => UserIdentitiesLazy.Value;

        private Lazy<IAttachmentsResource> AttachmentsLazy => new Lazy<IAttachmentsResource>(() => new AttachmentsResource(_apiClient, _logger));
        public IAttachmentsResource Attachments => AttachmentsLazy.Value;

        private Lazy<ITicketFieldsResource> TicketFieldsLazy => new Lazy<ITicketFieldsResource>(() => new TicketFieldsResource(_apiClient, _logger));
        public ITicketFieldsResource TicketFields => TicketFieldsLazy.Value;

        private Lazy<ITicketFormsResource> TicketFormsLazy => new Lazy<ITicketFormsResource>(() => new TicketFormsResource(_apiClient, _logger));
        public ITicketFormsResource TicketForms => TicketFormsLazy.Value;

        private Lazy<IOrganizationMembershipsResource> OrganizationMembershipsLazy => new Lazy<IOrganizationMembershipsResource>(() => new OrganizationMembershipsResource(_apiClient, _logger));
        public IOrganizationMembershipsResource OrganizationMemberships => OrganizationMembershipsLazy.Value;

        private Lazy<IRequestsResource> RequestLazy => new Lazy<IRequestsResource>(() => new RequestsResource(_apiClient, _logger));
        public IRequestsResource Requests => RequestLazy.Value;

        private Lazy<ISatisfactionRatingsResource> SatisfactionRatingLazy => new Lazy<ISatisfactionRatingsResource>(() => new SatisfactionRatingsResource(_apiClient, _logger));
        public ISatisfactionRatingsResource SatisfactionRatings => SatisfactionRatingLazy.Value;
    }
}
