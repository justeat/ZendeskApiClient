using System.Collections.Generic;
using System.Text;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Queries
{
    public class ZendeskQuery : IZendeskQuery
    {
        private readonly List<Filter> _customFilters = new List<Filter>();

        private SortBy _sortBy = SortBy.Relevance;
        private SortOrder _sortOrder = SortOrder.Desc;

        private static string EncodedSpace = System.Net.WebUtility.UrlEncode(" ");

        public IZendeskQuery WithFilter(string field, string value, FilterOperator filterOperator = FilterOperator.Equals)
        {
            _customFilters.Add(new Filter
            {
                Field = field,
                Value = value,
                FilterOperator = filterOperator
            });
            return this;
        }

        public IZendeskQuery WithFilter(int index, string field, string value, FilterOperator filterOperator = FilterOperator.Equals)
        {
            _customFilters.Insert(index, new Filter
            {
                Field = field,
                Value = value,
                FilterOperator = filterOperator
            });
            return this;
        }


        public IZendeskQuery WithOrdering(SortBy sortBy, SortOrder sortOrder)
        {
            _sortBy = sortBy;
            _sortOrder = sortOrder;
            return this;
        }

        public string BuildQuery()
        {
            var sb = new StringBuilder("query=");

            for (var i = 0; i < _customFilters.Count; i++)
            {
                var filter = _customFilters[i];
                var value = System.Net.WebUtility.UrlEncode(filter.Value);

                var operatorAndField = string.Empty;
                switch (filter.FilterOperator)
                {
                    case FilterOperator.Equals:
                        sb.AppendFormat("{0}:{1}", filter.Field, value);
                        break;
                    case FilterOperator.LessThan:
                        sb.AppendFormat("{0}<{1}", filter.Field, value);
                        break;
                    case FilterOperator.GreaterThan:
                        sb.AppendFormat("{0}>{1}", filter.Field, value);
                        break;
                    case FilterOperator.LessThanOrEqual:
                        sb.AppendFormat("{0}<={1}", filter.Field, value);
                        break;
                    case FilterOperator.GreaterThanOrEqual:
                        sb.AppendFormat("{0}>={1}", filter.Field, value);
                        break;
                    case FilterOperator.Exact:
                        sb.AppendFormat("{0}\"{1}\"", filter.Field == null ? null : filter.Field + ":", value);
                        break;
                    case FilterOperator.Excludes:
                        sb.AppendFormat("-{0}:{1}", filter.Field, value);
                        break;
                    case FilterOperator.Wildcard:
                        sb.AppendFormat("{0}:{1}*", filter.Field, value);
                        break;
                    case FilterOperator.None:
                        sb.Append(value);
                        break;
                    default:
                        break;
                }

                if (i < (_customFilters.Count - 1))
                {
                    sb.Append(EncodedSpace);
                }
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

            if (_sortOrder == SortOrder.Desc)
            {
                sb.Append("&sort_order=desc");
            }

            return sb.ToString();
        }
    }
}
