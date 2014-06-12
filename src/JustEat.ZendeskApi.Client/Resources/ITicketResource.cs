using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface ITicketResource
    {
        TicketResponse Get(long ticketId);
        ListResponse<Ticket> GetAll(List<long> ticketIds);
        TicketResponse Post(TicketRequest ticket);
    }
}