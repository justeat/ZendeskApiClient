using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class OrganizationFieldsCursorResponse : CursorPaginationResponse<OrganizationField>
    {
        [JsonProperty("organization_fields")]
        public IEnumerable<OrganizationField> OrganizationFields { get; internal set; }

        protected override IEnumerable<OrganizationField> Enumerable => OrganizationFields;
    }
}
