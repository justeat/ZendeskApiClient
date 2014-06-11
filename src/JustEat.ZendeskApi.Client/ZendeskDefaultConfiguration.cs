using System.Globalization;
using JE.Api.ClientBase;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskDefaultConfiguration : DefaultConfiguration
    {
        public ZendeskDefaultConfiguration()
        {
            Headers.AddHeader("Accept", "application/json");
            Headers.AddHeader("Content-Type", "application/json");
        }

        public string Authorization
        {
            get
            {
                return Headers.GetHeader("Authorization");
            }
            set
            {
                Headers.AddHeader("Authorization", value);
            }
        }
    }
}