using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Queries
{
    public class ZendeskZendeskQuery<T> : IZendeskQuery<T> where T : IZendeskEntity
    {
        private readonly Dictionary<string, string> _customFilters;

        private int _pageNumber = 1;

        private PageSize _pageSize = PageSize.Fifteen;

        private Order _order = Order.Desc;

        private OrderBy _orderBy = OrderBy.created_at;

        public ZendeskZendeskQuery()
        {
            _customFilters = new Dictionary<string, string>();
        }

        public IZendeskQuery<T> WithCustomFilter(string field, string value)
        {
            _customFilters.Add(field, value);
            return this;
        }

        public IZendeskQuery<T> WithPaging(int pageNumber, PageSize pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            return this;
        }

        public IZendeskQuery<T> WithOrdering(OrderBy orderBy, Order order)
        {
            _orderBy = orderBy;
            _order = order;
            return this;
        }

        public string BuildQuery()
        {
            var sb = new StringBuilder();

            var zendeskType = GetDescription(typeof (T));

            sb.Append(string.Format("query=type:{0}", zendeskType.ToLower()));

            foreach (var filter in _customFilters)
            {
                sb.Append(string.Format(" {0}:{1}", filter.Key, filter.Value));
            }

            sb.Append(string.Format("&sort_by={0}&sort_order={1}", _orderBy.ToString().ToLower(), _order.ToString().ToLower()));

            sb.Append(string.Format("&page={0}&per_page={1}", _pageNumber, (int)_pageSize));

            return sb.ToString();
        }

        static string GetDescription(Type type)
        {
            var descriptions = (DescriptionAttribute[])type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
            {
                return null;
            }
            return descriptions[0].Description;
        }
    }
}
