using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class CollaboratorNameEmailRequest : ICollaboratorRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public CollaboratorNameEmailRequest(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}