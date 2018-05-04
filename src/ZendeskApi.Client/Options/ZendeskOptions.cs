using System;

namespace ZendeskApi.Client.Options
{
    public class ZendeskOptions
    {
        public string EndpointUri { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string OAuthToken { get; set; }
        public TimeSpan? Timeout { get; set; }
    }
}
