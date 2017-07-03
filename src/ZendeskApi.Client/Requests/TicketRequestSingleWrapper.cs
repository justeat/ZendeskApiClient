using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class TicketRequestSingleWrapper<T>
    {
        public TicketRequestSingleWrapper(T ticketCreateRequest)
        {
            Ticket = ticketCreateRequest;
        }

        [JsonProperty("ticket")]
        public T Ticket { get; set; }
    }
}