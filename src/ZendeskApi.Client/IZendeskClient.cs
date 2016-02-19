﻿using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public interface IZendeskClient : IRestClient
    {
        ITicketResource Tickets { get; }
        ITicketCommentResource TicketComments { get; }
        IRequestCommentResource RequestComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupResource Groups { get; }
        IAssignableGroupResource AssignableGroups { get; }
        IUserResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IUploadResource Upload { get; }
        IOrganizationMembershipResource OrganizationMemberships { get; }
        IRequestResource Request { get; }
        ISatisfactionRatingResource SatisfactionRating { get; }
        IUploadResource Uploads { get; }
        ITicketFieldResource TicketFields { get; }
        ITicketFormResource TicketForms { get; }
        IJobStatusResource JobStatuses { get; }
    }
}