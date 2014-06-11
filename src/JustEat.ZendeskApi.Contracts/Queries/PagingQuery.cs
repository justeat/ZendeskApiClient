using System.Text;

namespace JustEat.ZendeskApi.Contracts.Queries
{
    public class PagingQuery : ZendeskQueryItem
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; } 

        public override StringBuilder AppendQuery(StringBuilder sb)
        {
            if (!PageNumber.HasValue || !PageSize.HasValue)
                return sb;

            var startChar = sb.Length > 0? "&" :"";
            sb.Append(string.Format("{0}page={1}&per_page={2}",startChar, PageNumber, PageSize));

            return sb;
        }
    }
}
