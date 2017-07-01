namespace ZendeskApi.Client.Models
{
    public static class TicketExtensions
    {
        public static Ticket WithRequester(this Ticket ticket, TicketRequester requester)
        {
            ticket.Requester = requester;
            return ticket;
        }

        public static Ticket WithComment(this Ticket ticket, TicketComment comment)
        {
            ticket.Comment = comment;
            return ticket;
        }
    }
}
