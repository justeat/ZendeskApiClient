using System.Collections.Generic;

namespace ZendeskApi.Client.Models
{
    public interface ICustomFields : IEnumerable<CustomField>
    {
        string this[long id] { get; set; }
    }
}