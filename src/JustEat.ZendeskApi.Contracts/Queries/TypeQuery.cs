using System.Text;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Queries
{
    public class TypeQuery : ZendeskQueryItem
    {
        public ZendeskType? Type { get; set; }
        public string CustomField { get; set; }
        public string CustomFieldValue { get; set; }

        public override StringBuilder AppendQuery(StringBuilder sb)
        {
            if (!Type.HasValue)
                return sb;

            sb.Append(string.Format("type:{0}", Type.ToString().ToLower()));

            if (string.IsNullOrEmpty(CustomField) || string.IsNullOrEmpty(CustomFieldValue))
                return sb;

            sb.Append(string.Format(" {0}:{1}", CustomField, CustomFieldValue));

            return sb;
        }
    }
}
