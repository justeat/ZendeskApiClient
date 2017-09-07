using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserFieldsResource
    {
        Task<IPagination<UserField>> GetAllAsync(PagerParameters pager = null);
        Task<UserField> GetAsync(long userFieldId);
        Task<UserField> CreateAsync(UserField userField);
        Task<UserField> UpdateAsync(UserField userField);
        Task DeleteAsync(long userFieldId);
    }
}
