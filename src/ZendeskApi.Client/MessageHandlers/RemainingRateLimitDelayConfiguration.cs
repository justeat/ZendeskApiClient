using System.Collections.Generic;

namespace ZendeskApi.Client.MessageHandlers
{
    public  class RemainingRateLimitDelayConfiguration
    {
        public List<RemainingRateLimitDelay> Delays { get; set; }
    }
}
