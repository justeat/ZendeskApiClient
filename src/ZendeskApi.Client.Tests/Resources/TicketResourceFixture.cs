using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketResourceFixture : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketResource _resource;

        public TicketResourceFixture()
        {
            _client = new DisposableZendeskApiClient();
            _resource = new TicketResource(_client);
        }

        [Fact]
        public async Task ShouldCreateTicket()
        {
            var response = await _resource.PostAsync(
                new Contracts.Requests.TicketRequest
                {
                    Item = new Contracts.Models.Ticket
                    {
                        Subject = "My printer is on fire!",
                        Comment = new Contracts.Models.TicketComment
                        {
                            Body = "The smoke is very colorful."
                        }
                    }
                });

            var item = response.Item;

            Assert.NotNull(item.Id);
            Assert.Equal("My printer is on fire!", item.Subject);
            Assert.Equal("The smoke is very colorful.", item.Comment.Body);
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var response = await _resource.GetAsync(435L);

            var item = response.Item;

            Assert.NotNull(item.Id);
            Assert.Equal("My printer is on fire!", item.Subject);
            Assert.Equal("The smoke is very colorful.", item.Comment.Body);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
