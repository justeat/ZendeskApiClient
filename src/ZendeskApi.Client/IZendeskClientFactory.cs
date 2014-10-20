using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Logging;
using ZendeskApi.Client.Serialization;

namespace ZendeskApi.Client
{
    public interface IZendeskClientFactory
    {
        IZendeskClient Create(Uri baseUri, ZendeskDefaultConfiguration configuration, ISerializer serializer = null, IHttpChannel httpChannel = null, ILogAdapter logger = null);
    }
}