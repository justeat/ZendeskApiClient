using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client
{
    public class ZendeskClient : IZendeskClient
    {
        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        public ZendeskClient(IZendeskApiClient apiClient, ILogger logger = null)
        {
            _apiClient = apiClient;
            _logger = logger ?? NullLogger.Instance;
        }

        private Lazy<ITicketsResource> TicketsLazy => new Lazy<ITicketsResource>(() => new TicketsResource(_apiClient, _logger));
        public ITicketsResource Tickets => TicketsLazy.Value;

        private Lazy<ITicketCommentsResource> TicketCommentsLazy => new Lazy<ITicketCommentsResource>(() => new TicketCommentsResource(_apiClient, _logger));
        public ITicketCommentsResource TicketComments => TicketCommentsLazy.Value;

        private Lazy<IDeletedTicketsResource> DeletedTicketsLazy => new Lazy<IDeletedTicketsResource>(() => new DeletedTicketsResource(_apiClient, _logger));
        public IDeletedTicketsResource DeletedTickets => DeletedTicketsLazy.Value;

        private Lazy<IOrganizationsResource> OrganizationsLazy => new Lazy<IOrganizationsResource>(() => new OrganizationsResource(_apiClient, _logger));
        public IOrganizationsResource Organizations => OrganizationsLazy.Value;

        private Lazy<ISearchResource> SearchLazy => new Lazy<ISearchResource>(() => new SearchResource(_apiClient, _logger));
        public ISearchResource Search => SearchLazy.Value;

        private Lazy<IGroupsResource> GroupsLazy => new Lazy<IGroupsResource>(() => new GroupsResource(_apiClient, _logger));
        public IGroupsResource Groups => GroupsLazy.Value;

        private Lazy<IUsersResource> UsersLazy => new Lazy<IUsersResource>(() => new UsersResource(_apiClient, _logger));
        public IUsersResource Users => UsersLazy.Value;

        private Lazy<IDeletedUsersResource> DeletedUsersLazy => new Lazy<IDeletedUsersResource>(() => new DeletedUsersResource(_apiClient, _logger));
        public IDeletedUsersResource DeletedUsers => DeletedUsersLazy.Value;

        private Lazy<IUserIdentityResource> UserIdentitiesLazy => new Lazy<IUserIdentityResource>(() => new UserIdentitiesResource(_apiClient, _logger));
        public IUserIdentityResource UserIdentities => UserIdentitiesLazy.Value;

        private Lazy<IAttachmentsResource> AttachmentsLazy => new Lazy<IAttachmentsResource>(() => new AttachmentsResource(_apiClient, _logger));
        public IAttachmentsResource Attachments => AttachmentsLazy.Value;

        private Lazy<ITicketFieldsResource> TicketFieldsLazy => new Lazy<ITicketFieldsResource>(() => new TicketFieldsResource(_apiClient, _logger));
        public ITicketFieldsResource TicketFields => TicketFieldsLazy.Value;

        private Lazy<ITicketFormsResource> TicketFormsLazy => new Lazy<ITicketFormsResource>(() => new TicketFormsResource(_apiClient, _logger));
        public ITicketFormsResource TicketForms => TicketFormsLazy.Value;

        private Lazy<ITicketAuditResource> TicketAuditsLazy => new Lazy<ITicketAuditResource>(() => new TicketAuditResource(_apiClient, _logger));
        public ITicketAuditResource TicketAudits => TicketAuditsLazy.Value;

        private Lazy<IOrganizationFieldsResource> OrganizationFieldsLazy => new Lazy<IOrganizationFieldsResource>(() => new OrganizationFieldsResource(_apiClient, _logger));
        public IOrganizationFieldsResource OrganizationFields => OrganizationFieldsLazy.Value;
        
        private Lazy<IOrganizationMembershipsResource> OrganizationMembershipsLazy => new Lazy<IOrganizationMembershipsResource>(() => new OrganizationMembershipsResource(_apiClient, _logger));
        public IOrganizationMembershipsResource OrganizationMemberships => OrganizationMembershipsLazy.Value;

        private Lazy<IRequestsResource> RequestLazy => new Lazy<IRequestsResource>(() => new RequestsResource(_apiClient, _logger));
        public IRequestsResource Requests => RequestLazy.Value;

        private Lazy<ISatisfactionRatingsResource> SatisfactionRatingLazy => new Lazy<ISatisfactionRatingsResource>(() => new SatisfactionRatingsResource(_apiClient, _logger));
        public ISatisfactionRatingsResource SatisfactionRatings => SatisfactionRatingLazy.Value;

        private Lazy<IUserFieldsResource> UserFieldsLazy => new Lazy<IUserFieldsResource>(() => new UserFieldsResource(_apiClient, _logger));
        public IUserFieldsResource UserFields => UserFieldsLazy.Value;

        private Lazy<IJobStatusResource> JobStatusesLazy =>
            new Lazy<IJobStatusResource>(() => new JobStatusResource(_apiClient, _logger));

        public IJobStatusResource JobStatuses => JobStatusesLazy.Value;

        private Lazy<IServiceStatusResource> ServiceStatusLazy => new Lazy<IServiceStatusResource>(() => new ServiceStatusResource(_apiClient, _logger));
        public IServiceStatusResource ServiceStatus => ServiceStatusLazy.Value;
        
        private Lazy<IHelpCenterResource> HelpCenterLazy =>
            new Lazy<IHelpCenterResource>(() => new HelpCenterResource(_apiClient, _logger));
        public IHelpCenterResource HelpCenter => HelpCenterLazy.Value;

        private Lazy<ILocaleResource> LocalesLazy =>
            new Lazy<ILocaleResource>(() => new LocaleResource(_apiClient, _logger));
        public ILocaleResource Locales => LocalesLazy.Value;

        private Lazy<ITagsResource> TagsLazy => new Lazy<ITagsResource>(() => new TagsResource(_apiClient, _logger));
        public ITagsResource Tags => TagsLazy.Value;
    }
}
