using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Queries
{
    public class ZendeskQuery<T> : IZendeskQuery<T> where T : IZendeskEntity
    {
        private readonly List<Filter> _customFilters;

        private int _pageNumber = 1;

        private int _pageSize = 15;

        private Order _order = Order.Desc;

        private OrderBy _orderBy = OrderBy.created_at;

        public ZendeskQuery()
        {
            _customFilters = new List<Filter>();
        }

        public IZendeskQuery<T> WithCustomFilter(string field, string value, FilterOperator filterOperator = FilterOperator.Equals)
        {
            Filter nFilter = new Filter();
            nFilter.Field = field;
            nFilter.Value = value;
            nFilter.FilterOperator = filterOperator;
            _customFilters.Add(nFilter);
            return this;
        }

        public IZendeskQuery<T> WithPaging(int pageNumber, int pageSize)
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
                string operatorString = String.Empty;
                switch (filter.FilterOperator)
                {
                    case FilterOperator.LessThan:
                        operatorString = "<";
                        break;
                    case FilterOperator.GreaterThan:
                        operatorString = ">";
                        break;
                    case FilterOperator.Equals:
                        operatorString = ":";
                        break;
                    case FilterOperator.NotEqual:
                        operatorString = ":-";
                        break;
                    default:
                        break;
                }
                sb.Append(string.Format("{0}{1}{2}{3}", HttpUtility.UrlEncode(" "), filter.Field, operatorString, HttpUtility.UrlEncode(filter.Value)));
            }
            sb.Append(string.Format("&sort_by={0}&sort_order={1}", _orderBy.ToString().ToLower(), _order.ToString().ToLower()));

            sb.Append(string.Format("&page={0}&per_page={1}", _pageNumber, _pageSize));

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
