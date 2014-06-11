using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface ITicketResource
    {
        TicketResponse Get(int ticketId);
        ListResponse<Ticket> GetAll(List<int> ticketIds);
        TicketResponse Post(TicketRequest ticket);
    }
}