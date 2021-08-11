namespace ZendeskApi.Client.Models
{
    public class CursorPagerVariant
    {
        public string Cursor { get; set; } = null;
        public int ResultsLimit { get; set; } = 100;
    }
}