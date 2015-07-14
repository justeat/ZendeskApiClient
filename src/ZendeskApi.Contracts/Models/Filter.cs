namespace ZendeskApi.Contracts.Models
{
    public struct Filter
    {
        public string field;
        public string value;
        public FilterOperator filterOperator;
    }
}