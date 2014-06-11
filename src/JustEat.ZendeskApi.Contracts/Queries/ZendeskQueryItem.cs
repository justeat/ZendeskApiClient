using System.Text;

namespace JustEat.ZendeskApi.Contracts.Queries
{
    public abstract class ZendeskQueryItem
    {
        public abstract StringBuilder AppendQuery(StringBuilder sb);
    }
}
