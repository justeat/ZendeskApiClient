using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketFieldsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketFieldsResource _resource;

        public TicketFieldsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new TicketFieldsResourceSampleSite(resource));
            _resource = new TicketFieldsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllTicketFields()
        {
            var obj1 = await _resource.CreateAsync(new TicketField
            {
                RawTitle = "FuBar"
            });

            var obj2 = await _resource.CreateAsync(new TicketField
            {
                RawTitle = "FuBar2"
            });

            var retrievedGroups = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldGetTicketFieldById()
        {
            var obj1 = await _resource.CreateAsync(new TicketField
            {
                RawTitle = "FuBar"
            });

            var obj2 = await _resource.CreateAsync(new TicketField
            {
                RawTitle = "FuBar2"
            });

            var obj3 = await _resource.GetAsync(obj2.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(obj3));
        }

        [Fact]
        public async Task ShouldCreateTicketField()
        {
            var response = await _resource.CreateAsync(
                new TicketField
                {
                    RawTitle = "FuBar"
                });

            Assert.NotNull(response.Id);
            Assert.Equal("FuBar", response.RawTitle);
        }

        [Fact]
        public async Task ShouldUpdateTicketField()
        {
            var ticketField = await _resource.CreateAsync(
                new TicketField
                {
                    RawTitle = "FuBar"
                });

            Assert.Equal("FuBar", ticketField.RawTitle);

            ticketField.RawTitle = "BarFu";

            ticketField = await _resource.UpdateAsync(ticketField);

            Assert.Equal("BarFu", ticketField.RawTitle);
        }

        [Fact]
        public async Task ShouldDeleteTicketField()
        {
            var ticketField = await _resource.CreateAsync(
                new TicketField
                {
                    RawTitle = "FuBar"
                });

            var ticketField1 = await _resource.GetAsync(ticketField.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(ticketField), JsonConvert.SerializeObject(ticketField1));

            await _resource.DeleteAsync(ticketField.Id.Value);

            var ticketField2 = await _resource.GetAsync(ticketField.Id.Value);

            Assert.Null(ticketField2);
        }
    }
}
