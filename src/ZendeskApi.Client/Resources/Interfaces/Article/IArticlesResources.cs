using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IArticlesResources
    {
        Task<IPagination<Article>> ListAsync(string query, PagerParameters pager = null);
    }
}
