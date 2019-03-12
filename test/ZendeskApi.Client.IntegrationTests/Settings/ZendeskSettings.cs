using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskApi.Client.IntegrationTests.Settings
{
    public class ZendeskSettings
    {
        public string Url { get; }
        public string Username { get; }
        public string Token { get; }

        public ZendeskSettings()
        {
            Url = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Url");
            Username = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Username");
            Token = Environment.GetEnvironmentVariable("ZendeskApi_Credentials_Token");
        }
    }
}
