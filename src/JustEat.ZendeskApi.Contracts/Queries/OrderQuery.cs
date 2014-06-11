using System.Text;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Queries
{
    public class OrderQuery : ZendeskQueryItem
    {
        public OrderBy? OrderBy { get; set; }
        public Order Order { get; set; }

        public override StringBuilder AppendQuery(StringBuilder sb)
        {
            if (!OrderBy.HasValue)
                return sb;

            var startChar = sb.Length > 0 ? "&" : "";
            sb.Append(string.Format("{0}sort_by={1}&sort_order={2}", startChar, OrderBy, Order.ToString().ToLower()));

            return sb;
        }
    }
}
