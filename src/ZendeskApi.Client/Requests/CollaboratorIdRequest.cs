namespace ZendeskApi.Client.Requests
{
    public class CollaboratorIdRequest : ICollaboratorRequest
    {
        public long Id { get; set; }

        public CollaboratorIdRequest(long id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}