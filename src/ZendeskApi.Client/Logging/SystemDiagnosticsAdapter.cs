namespace ZendeskApi.Client.Logging
{
    public class SystemDiagnosticsAdapter : ILogAdapter
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(message, "ZendeskApi.Client.Logging.Trace");
        }

        public void Warn(string message)
        {
            System.Diagnostics.Trace.WriteLine(message, "ZendeskApi.Client.Logging.Warn");
        }

        public void Info(string message)
        {
            System.Diagnostics.Trace.WriteLine(message, "ZendeskApi.Client.Logging.Info");
        }

        public void Debug(string message)
        {
            System.Diagnostics.Trace.WriteLine(message, "ZendeskApi.Client.Logging.Debug");
        }

        public void Error(string message)
        {
            System.Diagnostics.Trace.WriteLine(message, "ZendeskApi.Client.Logging.Error");
        }
    }
}
