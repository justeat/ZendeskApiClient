using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;

namespace JustEat.ZendeskApi.Client
{
    public interface IZendeskClientFactory
    {
        IZendeskClient Create(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ILogAdapter logger = null);
    }
}