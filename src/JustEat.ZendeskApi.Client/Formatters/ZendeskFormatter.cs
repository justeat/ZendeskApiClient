using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace JustEat.ZendeskApi.Client.Formatters
{
    public static class ZendeskFormatter
    {
        public static string ToCsv(IEnumerable<string> items)
        {
            return string.Join(",", items.Select(i => i.Trim()));
        }

        public static string ToCsv(IEnumerable<long> items)
        {
            return string.Join(",", items.Select(i => i.ToString(CultureInfo.InvariantCulture).Trim()));
        }
    }
}
