namespace JustEat.ZendeskApi.Client.Serialization
{
    public interface ISerializer
    {
        T Deserialize<T>(string data);

        string Serialize(object obj);
    }
}