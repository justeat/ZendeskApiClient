using System.Collections.Generic;

namespace ZendeskApi.Client.Models
{
    public interface IReadOnlyCustomFields : IReadOnlyList<CustomField>
    {
        string this[long id] { get; }

        List<string> GetValues(long id);

        void SetValues(long id, List<string> values);
    }
}