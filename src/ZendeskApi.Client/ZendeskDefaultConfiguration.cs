using System;
using System.Text;
using ZendeskApi.Client.Configuration;

namespace ZendeskApi.Client
{
    public class ZendeskDefaultConfiguration
    {
        private const string AcceptKey = "Accept";
        private const string Authorization = "Authorization";
        private const string ContentTypeKey = "Content-Type";
        private const string AcceptCharset = "Accept-Charset";

        public Headers Headers { get; private set; }

        public TimeSpan? RequestTimeout { get; set; }

        public ZendeskDefaultConfiguration(string username, string token)
        {
            Headers = new Headers();

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}/token:{1}", username, token)));

            Headers.AddHeader(AcceptCharset, "utf-8");
            Headers.AddHeader(Authorization, string.Format("Basic {0}", auth));
            Headers.AddHeader(AcceptKey, "application/json");
            Headers.AddHeader(ContentTypeKey, "application/json");
        }
    }
}