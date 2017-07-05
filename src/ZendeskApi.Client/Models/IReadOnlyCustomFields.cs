using System.Collections.Generic;

namespace ZendeskApi.Client.Models
{
    public interface IReadOnlyCustomFields : IReadOnlyList<CustomField>
    {
        string this[long id] { get; }
    }
}