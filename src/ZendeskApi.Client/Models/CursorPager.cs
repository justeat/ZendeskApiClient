namespace ZendeskApi.Client.Models
{
    public class CursorPager
    {
        public string Cursor { get; set; } = null;
        public int ResultsLimit { get; set; } = 250;
    }
}