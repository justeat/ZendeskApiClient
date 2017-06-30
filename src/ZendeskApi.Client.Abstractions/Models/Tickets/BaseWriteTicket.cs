using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Tickets
{
    public abstract class BaseWriteTicket : BaseTicket
    {
        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }

        [JsonProperty("brand_id")]
        public long? BrandId { get; set; }

        public TicketComment Comment { get; set; }
    }
}