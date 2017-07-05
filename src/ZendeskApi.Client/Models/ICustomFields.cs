using System.Collections.Generic;

namespace ZendeskApi.Client.Models
{
    public interface ICustomFields : IList<CustomField>
    {
        string this[long id] { get; set; }
    }
}