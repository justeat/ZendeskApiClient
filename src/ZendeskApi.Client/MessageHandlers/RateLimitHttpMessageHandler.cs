using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ZendeskApi.Client.MessageHandlers
{
    public class RateLimitHttpMessageHandler : DelegatingHandler
    {

        private readonly RemainingRateLimitDelayConfiguration _remainingRateDelayConfiguration;

        private readonly ILogger<RateLimitHttpMessageHandler> _logger;
        public RateLimitHttpMessageHandler(ILogger<RateLimitHttpMessageHandler> logger, RemainingRateLimitDelayConfiguration rateLimitConfig)
        {
            _logger = logger;
            _remainingRateDelayConfiguration = rateLimitConfig;
            
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            var result = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (result != null)
            {
                IEnumerable<string> remainingLimitString = null;
                if (result.Headers.TryGetValues("x-rate-limit-remaining", out remainingLimitString))
                {
                    int remainingLimit = -1;
                    if (remainingLimitString?.Any() == true)
                    {
                        int.TryParse(remainingLimitString.First(), out remainingLimit);
                        if (_remainingRateDelayConfiguration.Delays.Any(x => x.RemainingLimit > remainingLimit))
                        {
                            var delay = _remainingRateDelayConfiguration.Delays.OrderBy(x => x.RemainingLimit)
                                .FirstOrDefault(x => x.RemainingLimit > remainingLimit)?.DelayInSeconds;
                            if (delay.HasValue && delay.Value > 0)
                            {
                                _logger.LogInformation(
                                    "Remaining rate limit is {remainingLimit} - pausing for {delay}s.", remainingLimit,
                                    delay);

                                await Task.Delay(TimeSpan.FromSeconds(delay.Value));
                            }
                        }
                        
                    }
                }
            }

            return result;
        }
    }
}
