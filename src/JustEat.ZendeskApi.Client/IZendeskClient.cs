using System;
using System.Web;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client
{
    public interface IZendeskClient : IBaseClient
    {
        ITicketResource Tickets { get; }
        ITicketCommentResource TicketComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupResource Groups { get; }
        IAssignableGroupResource AssignableGroups { get; }
        IUserResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IOrganizationMembershipResource OrganizationMemberships { get; }
        IUploadResource Uploads { get; }
        Uri BuildZendeskUri(string handler, string query = "");
    }
}