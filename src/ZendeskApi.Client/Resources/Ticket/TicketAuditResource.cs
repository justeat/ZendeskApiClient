using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketAuditResource : AbstractBaseResource<TicketAuditResource>, ITicketAuditResource
    {
        private const string ResourceUri = "api/v2/ticket_audits";
        private const string TicketAuditsUri = "api/v2/tickets/{0}/audits";
        private const string SpecificTicketAuditUri = "api/v2/tickets/{0}/{1}";
        
        public TicketAuditResource(IZendeskApiClient apiClient, ILogger logger) : base(apiClient, logger,
            "ticket_audits")
        {
        }

        public async Task<TicketAuditResponse> GetAllAsync(CursorPagerVariant pager = null, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketAuditResponse>(
                ResourceUri,
                "list-all-ticket-audits",
                "GetAllAsync",
                pager,
                cancellationToken);
        }

        public async Task<TicketAuditResponse> GetAllByTicketAsync(long ticketId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TicketAuditResponse>(
                string.Format(TicketAuditsUri, ticketId),
                "list-audits-for-a-ticket",
                $"GetAllByTicketAsync({ticketId}",
                null,
                null,
                cancellationToken);
        }

        public async Task<SingleTicketAuditResponse> Get(int ticketId, int auditId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<SingleTicketAuditResponse>(
                string.Format(SpecificTicketAuditUri, ticketId, auditId),
                "show-audit",
                $"Get({ticketId}, {auditId})",
                null,
                null,
                cancellationToken);
        }
    }
}