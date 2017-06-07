using System.Net.Http;

namespace ZendeskApi.Client
{
    public interface IZendeskApiClient
    {
        HttpClient CreateClient(string resource = null);
    }
}
