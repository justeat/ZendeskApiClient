using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IJobStatusResource
    {
        #region List
        
        /// <summary>
        /// Shows the current statuses for background jobs running.
        /// </summary>
        Task<IPagination<JobStatusResponse>> ListAsync(PagerParameters pagerParameters = null);
        
        #endregion

        #region Show

        Task<JobStatusResponse> GetAsync(string statusId);
        Task<IPagination<JobStatusResponse>> GetAsync(string[] statusIds, PagerParameters pagerParameters = null);        

        #endregion
    }
}