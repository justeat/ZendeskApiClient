using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClientFactory : IZendeskClientFactory
    {
        public IZendeskClient Create(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ILogAdapter logger = null)
        {
            return new ZendeskClient(baseUri, configuration, httpChannel, logger);
        }
    }
}
