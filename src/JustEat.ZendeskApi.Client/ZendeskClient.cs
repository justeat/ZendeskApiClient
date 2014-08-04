﻿using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;
using JE.Api.ClientBase.Serialization;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClient: BaseClient, IZendeskClient
    {
        public ITicketResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Groups { get; private set; }
        public IAssignableGroupResource AssignableGroups { get; private set; }
        public IUserResource Users { get; private set; }
        public IUserIdentityResource UserIdentities { get; private set; }
        public IOrganizationMembershipResource OrganizationMemberships { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ILogAdapter logger = null, ISerializer serializer = null)
            : base(baseUri, configuration, httpChannel, serializer ?? new ZendeskJsonSerializer(), logger)
        {
            Tickets = new TicketResource(this);
            TicketComments = new TicketCommentResource(this);
            Organizations = new OrganizationResource(this);
            Search = new SearchResource(this);
            Groups = new GroupsResource(this);
            AssignableGroups = new AssignableGroupResource(this);
            Users = new UserResource(this);
            UserIdentities = new UserIdentityResource(this);
            OrganizationMemberships = new OrganizationMembershipResource(this);
        }
    }
}
