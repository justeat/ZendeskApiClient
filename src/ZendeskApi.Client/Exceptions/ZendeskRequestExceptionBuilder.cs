using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Exceptions
{
    internal class ZendeskRequestExceptionBuilder
    {
        private HttpResponseMessage _response;
        private HttpStatusCode? _expectedStatusCode;
        private string _helpDocsResource;

        public ZendeskRequestExceptionBuilder()
        {
            
        }

        public ZendeskRequestExceptionBuilder WithResponse(HttpResponseMessage response)
        {
            this._response = response;
            return this;
        }
        public ZendeskRequestExceptionBuilder WithExpectedHttpStatus(HttpStatusCode status)
        {
            this._expectedStatusCode = status;
            return this;
        }
        public ZendeskRequestExceptionBuilder WithHelpDocsLink(string link)
        {
            this._helpDocsResource = link;
            return this;
        }

        public async Task<ZendeskRequestException> Build()
        {
            var message = new StringBuilder();
            ErrorResponse error = null;

            if (_response != null && (int)_response.StatusCode <= 499)
            {
                error = await _response.Content.ReadAsAsync<ErrorResponse>();

                if (error?.Error != null && error.Description != null)
                {
                    message.AppendLine($"{error.Error}: {error.Description}.");
                }
            }

            if (_expectedStatusCode.HasValue)
            {
                message.AppendLine(_response != null
                    ? $"Status code retrieved was {_response.StatusCode} and not {_expectedStatusCode.Value}({(int) _expectedStatusCode.Value}) as expected."
                    : $"Status code retrieved was not {_expectedStatusCode.Value} as expected.");
            }
            else if (_response != null)
            {
                message.AppendLine($"Status code retrieved was {_response.StatusCode}.");
            }

            if (_helpDocsResource != null)
            {
                message.AppendLine($"See: https://developer.zendesk.com/rest_api/docs/{_helpDocsResource} .");
            }

            return new ZendeskRequestException(message.ToString(), error, _response);
        }

    }
}