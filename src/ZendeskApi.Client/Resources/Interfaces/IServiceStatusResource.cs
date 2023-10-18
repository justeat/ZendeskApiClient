using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models.Status.Response;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IServiceStatusResource
    {
        Task<ListIncidentsResponse> ListActiveIncidents(string subdomain = null, CancellationToken cancellationToken = default);
        Task<IncidentResponse> GetActiveIncident(string incidentId, CancellationToken cancellationToken = default);
        Task<ListMaintenanceIncidentsResponse> ListMaintenanceIncidents(string subdomain = null, CancellationToken cancellationToken = default);
    }
}
