using System.Collections.Generic;
using System.Linq;

namespace ZendeskApi.Client.Models
{
    public class CustomFields : List<CustomField>, IReadOnlyCustomFields, ICustomFields
    {
        public CustomFields()
        {
            
        }

        public CustomFields(Dictionary<long, List<string>> fields)
        {
            foreach (var field in fields)
            {
                this[field.Key] = field.Value;
            }
        }

        private void Set(long id, List<string> value)
        {
            if (this.All(cf => cf.Id != id))
            {
                Add(new CustomField
                {
                    Id = id,
                    Value = value
                });
            }
            else
            {
                this.FirstOrDefault(cf => cf.Id == id).Value = value;
            }
        }

        private List<string> Get(long id)
        {
            return this.FirstOrDefault(cf => cf.Id == id)?.Value;
        }

        public List<string> this[long id]
        {
            get => Get(id);
            set => Set(id, value);
        }
    }
}
