using JE.Api.ClientBase;

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
