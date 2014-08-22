namespace JustEat.ZendeskApi.Client.Logging
{
    public interface ILogAdapter
    {
        void Trace(string message);

        void Warn(string message);

        void Info(string message);

        void Debug(string message);

        void Error(string message);
    }
}