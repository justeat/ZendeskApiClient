using System.Collections.Generic;
using System.Linq;

namespace ZendeskApi.Client.Models
{
    public class CustomFields : List<CustomField>, ICustomFields
    {
        public CustomFields()
        {
            
        }

        public CustomFields(Dictionary<long, string> fields)
        {
            foreach (var field in fields)
            {
                this[field.Key] = field.Value;
            }
        }

        private void Set(long id, string value)
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

        private string Get(long id)
        {
            return this.FirstOrDefault(cf => cf.Id == id)?.Value;
        }

        public string this[long id]
        {
            get => Get(id);
            set => Set(id, value);
        }
    }
}
