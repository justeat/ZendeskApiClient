using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Queries
{
    public class ZendeskQuery<T> : IZendeskQuery<T>
    {
        private readonly List<Filter> _customFilters;

        private int _pageNumber = 1;

        private int _pageSize = 15;

        private SortOrder _sortOrder = SortOrder.Desc;

        private SortBy _sortBy = SortBy.Relevance;

        public ZendeskQuery()
        {
            _customFilters = new List<Filter>();
        }

        public IZendeskQuery<T> WithCustomFilter(string field, string value, FilterOperator filterOperator = FilterOperator.Equals)
        {
            var nFilter = new Filter()
            {
                Field = field,
                Value = value,
                FilterOperator = filterOperator
            };
            _customFilters.Add(nFilter);
            return this;
        }

        public IZendeskQuery<T> WithPaging(int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            return this;
        }

        public IZendeskQuery<T> WithOrdering(SortBy sortBy, SortOrder sortOrder)
        {
            _sortBy = sortBy;
            _sortOrder = sortOrder;
            return this;
        }

        public string BuildQuery()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("query=type:{0}", typeof(T).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id));

            foreach (var filter in _customFilters)
            {
                var operatorAndField = string.Empty;
                switch (filter.FilterOperator)
                {
                    case FilterOperator.LessThan:
                        operatorAndField = string.Format("{0}<", filter.Field);
                        break;
                    case FilterOperator.GreaterThan:
                        operatorAndField = string.Format("{0}>", filter.Field);
                        break;
                    case FilterOperator.Equals:
                        operatorAndField = string.Format("{0}:", filter.Field);
                        break;
                    case FilterOperator.NotEqual:
                        operatorAndField = string.Format("-{0}:", filter.Field);
                        break;
                    default:
                        break;
                }

                sb.Append(string.Format("{0}{1}{2}", System.Net.WebUtility.UrlEncode(" "), operatorAndField, System.Net.WebUtility.UrlEncode(filter.Value)));
            }

            if (_sortBy != SortBy.Relevance)
            {
                switch(_sortBy)
                {
                    case SortBy.CreatedAt:
                        sb.Append("&sort_by=created_at");
                        break;
                    case SortBy.Priority:
                        sb.Append("&sort_by=priority");
                        break;
                    case SortBy.UpdateAt:
                        sb.Append("&sort_by=updated_at");
                        break;
                    case SortBy.TicketType:
                        sb.Append("&sort_by=ticket_type");
                        break;
                    case SortBy.Status:
                        sb.Append("&sort_by=status");
                        break;
                }
            }

            if (_sortOrder == SortOrder.Asc)
            {
                sb.Append("&sort_order=asc");
            }

            sb.Append(string.Format("&page={0}&per_page={1}", _pageNumber, _pageSize));

            return sb.ToString();
        }
    }
}
