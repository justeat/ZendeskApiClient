using System.Collections.Generic;
using System.Text;
using JustEat.ZendeskApi.Contracts.Queries;

namespace JustEat.ZendeskApi.Client.Factories
{
    public class QueryFactory : IQueryFactory
    {
        private readonly TypeQuery _type;
        private readonly OrderQuery _order;
        private readonly PagingQuery _paging;

        private List<string> _query;

        private readonly StringBuilder _builder;

        public QueryFactory(TypeQuery type = null, OrderQuery order = null, PagingQuery paging = null)
        {
            _type = type ?? new TypeQuery();
            _order = order ?? new OrderQuery();
            _paging = paging ?? new PagingQuery();
            _builder = new StringBuilder();
            _query = new List<string>();
        }

        public string BuildQuery()
        {
            _type.AppendQuery(_builder);

            _order.AppendQuery(_builder);

            _paging.AppendQuery(_builder);

            if (_builder.Length > 0)
                _builder.Insert(0, "query=");

            return _builder.ToString();
        }
    }
}
