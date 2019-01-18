using System.Collections.Generic;

namespace ZendeskApi.Client.Models
{
    public interface ICustomFields : IList<CustomField>
    {
        List<string> this[long id] { get; set; }
    }
}