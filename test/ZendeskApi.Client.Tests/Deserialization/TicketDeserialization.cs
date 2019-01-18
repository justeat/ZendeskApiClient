using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Deserialization
{
    public class TicketDeserialization
    {
        [Fact]
        public void TicketCorrectlyDeserializes_CustomFieldsWith_ValueArrays()
        {           
            var ticketJson = File.ReadAllText(AppContext.BaseDirectory  + "/Deserialization/ticket.json");
            var ticket = JsonConvert.DeserializeObject<Ticket>(ticketJson);

            var field = ticket.CustomFields.FirstOrDefault(x => x.Id == 360000027769);

            Assert.Contains("fd_1st_january", field.Value);
            Assert.Contains("fd_2nd_january", field.Value);
        }
    }
}
