using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ZendeskApi.Client.MessageHandlers
{
    public class RetryRateLimitHttpMessageHandler : DelegatingHandler 
    {
        private readonly ILogger<RetryRateLimitHttpMessageHandler> _logger;
        public RetryRateLimitHttpMessageHandler(ILogger<RetryRateLimitHttpMessageHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage result = null;
            for (int i = 0; i < 3; i++)
            {

                result = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                if (result != null && result.IsSuccessStatusCode)
                {
                    break;
                }
                
                if (result != null && (int)result.StatusCode == 429)
                {
                    var delay = 5 * (i + 1);

                    if (result.Headers?.RetryAfter != null)
                    {
                        var delayValue = result.Headers.RetryAfter;
                        if (delayValue != null && delayValue.Delta.HasValue)
                        {
                            delay = (int)delayValue.Delta.Value.TotalSeconds;
                        }
                    }

                    _logger?.LogWarning("Rate limited response catched by HTTP Message handler - waiting {delay} seconds.", delay);

                    await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
