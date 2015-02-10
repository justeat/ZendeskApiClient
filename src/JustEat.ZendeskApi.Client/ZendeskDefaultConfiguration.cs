using System;
using System.Collections.Generic;
using System.Text;
using JE.Api.ClientBase;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskDefaultConfiguration : DefaultConfiguration
    {
        public ZendeskDefaultConfiguration(string username, string token)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}/token:{1}", username, token)));

            Headers.AddHeader("Authorization", string.Format("Basic {0}", auth));

        }
    }
}