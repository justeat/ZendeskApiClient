using System;
using System.Text;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Extensions
{
    public static class AuthorizationHeaderExtensions
    {
        public static string GetAuthorizationHeader(this ZendeskOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.Token) && !string.IsNullOrWhiteSpace(options.Username))
            {
                return GetBasicAuthHeader(options.Username, options.Token);
            }

            if (!string.IsNullOrWhiteSpace(options.OAuthToken))
            {
                return GetBearerAuthHeader(options.OAuthToken);
            }

            return string.Empty;
        }

        private static string GetBearerAuthHeader(string oAuthToken)
        {
            return $"Bearer {oAuthToken}";
        }

        private static string GetBasicAuthHeader(string userName, string token)
        {
            var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}/token:{token}"));
            return $"Basic {authorizationHeader}";
        }
    }
}