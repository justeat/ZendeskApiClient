namespace ZendeskApi.Client.Requests
{
    public class CollaboratorEmailRequest : ICollaboratorRequest
    {
        public string Email { get; set; }

        public CollaboratorEmailRequest(string email)
        {
            Email = email;
        }

        public override string ToString()
        {
            return Email;
        }
    }
}