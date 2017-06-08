﻿using System;
using System.Net.Http;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public abstract class SampleSite : IDisposable
    {
        public abstract HttpClient Client { get; }

        public abstract void RefreshClient(string resource);

        public abstract void Dispose();
    }
}
