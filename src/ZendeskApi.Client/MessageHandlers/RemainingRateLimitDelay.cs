using System;

namespace ZendeskApi.Client.MessageHandlers
{
    public class RemainingRateLimitDelay
    {
        public RemainingRateLimitDelay()
        {

        }

        public RemainingRateLimitDelay(int remainingLimit, double delayInSeconds)
        {
            RemainingLimit = remainingLimit;
            DelayInSeconds = delayInSeconds;
        }

        public int RemainingLimit { get; set; }
        public double DelayInSeconds { get; set; }
    }
}
