using System;
using System.IO;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Deserialization
{
    public class TicketDeserialization
    {
        [Fact]
        public void TicketDeserializationTest()
        {           
            var ticketJson = File.ReadAllText(AppContext.BaseDirectory  + "/Deserialization/ticket.json");
            var ticket = JsonConvert.DeserializeObject<Ticket>(ticketJson);
        }
    }
}
