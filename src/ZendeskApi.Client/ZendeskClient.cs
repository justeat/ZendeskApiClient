using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Logging;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Serialization;

namespace ZendeskApi.Client
{
    public class ZendeskClient : ClientBase, IZendeskClient
    {
        public ITicketResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IRequestCommentResource RequestComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Groups { get; private set; }
        public IAssignableGroupResource AssignableGroups { get; private set; }
        public IUserResource Users { get; private set; }
        public IUserIdentityResource UserIdentities { get; private set; }
        public IUploadResource Upload { get; private set; }
        public IOrganizationMembershipResource OrganizationMemberships { get; private set; }
        public IRequestResource Request { get; private set; }
        public ISatisfactionRatingResource SatisfactionRating { get; private set; }
        public IUploadResource Uploads { get; private set; }
        public ITicketFieldResource TicketFields { get; private set; }
        public ITicketFormResource TicketForms { get; private set; }
        public IJobStatusResource JobStatuses { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, ISerializer serializer = null, IHttpChannel httpChannel = null, ILogAdapter logger = null)
            : base(baseUri, configuration, serializer, httpChannel, logger)
        {
            Tickets = new TicketResource(this);
            TicketComments = new TicketCommentResource(this);
            RequestComments = new RequestCommentResource(this);
            Organizations = new OrganizationResource(this);
            Search = new SearchResource(this);
            Groups = new GroupsResource(this);
            AssignableGroups = new AssignableGroupResource(this);
            Users = new UserResource(this);
            UserIdentities = new UserIdentityResource(this);
            Upload = new UploadResource(this);
            OrganizationMemberships = new OrganizationMembershipResource(this);
            Request = new RequestResource(this);
            SatisfactionRating = new SatisfactionRatingResource(this);
            Uploads = new UploadResource(this);
            TicketFields = new TicketFieldResource(this);
            TicketForms = new TicketFormResource(this);
            JobStatuses = new JobStatusResource(this);
        }

        public Uri BuildZendeskUri(string handler, string query = "")
        {
            return new UriBuilder(BaseUri)
            {
                Path = handler + ".json",
                Query = query
            }.Uri;
        }
    }
}
