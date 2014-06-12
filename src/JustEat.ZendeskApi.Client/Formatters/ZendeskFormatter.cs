using System.Collections.Generic;
using System.Linq;

namespace JustEat.ZendeskApi.Client.Formatters
{
    public static class ZendeskFormatter
    {
        public static string ToCsv(List<string> items)
        {
            return string.Join(",", items.Select(i => i.Trim()));
        }

        public static string ToCsv(List<long> items)
        {
            return string.Join(",", items.Select(i => i.ToString().Trim()));
        }
    }
}
