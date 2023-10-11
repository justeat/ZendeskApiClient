namespace ZendeskApi.Client.Models
{
    public class CursorPager
    {
        public int Size { get; set; } = 100;
        public string BeforeCursor { get; set; }
        public string AfterCursor { get; set; }

    }
}