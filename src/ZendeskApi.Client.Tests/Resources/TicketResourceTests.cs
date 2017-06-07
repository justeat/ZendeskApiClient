using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketResource _resource;

        public TicketResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new TicketResourceSampleSite(resource));
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

            Assert.NotNull(response.Id);
            Assert.Equal("My printer is on fire!", response.Subject);
            Assert.Equal("The smoke is very colorful.", response.Comment.Body);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201()
        {
            return Assert.ThrowsAsync<HttpRequestException>(async () => await _resource.PostAsync(
                new Contracts.Requests.TicketRequest
                {
                    Item = new Contracts.Models.Ticket
                    {
                        Subject = "My printer is no longer on fire!",
                        Comment = new Contracts.Models.TicketComment
                        {
                            Body = "The smoke is gone."
                        },
                        Tags = new System.Collections.Generic.List<string> { "error" }
                    }
                }));

            // could use tags to simulate httpstatus codes in fake client?
        }

        [Fact]
        public async Task ShouldJobWhenCreatingMultipleTicket()
        {
            var response = await _resource.PostAsync(
                new Contracts.Requests.TicketsRequest
                {
                    Item = new[] {
                        new Contracts.Models.Ticket {
                            Subject = "My printer is on fire!",
                            Comment = new Contracts.Models.TicketComment
                            {
                                Body = "The smoke is very colorful."
                            }
                        },
                        new Contracts.Models.Ticket {
                            Subject = "My printer is somehow on fire again!",
                            Comment = new Contracts.Models.TicketComment
                            {
                                Body = "The smoke is not very colorful."
                            }
                        }
                    }
                });
            
            Assert.NotNull(response.Id);
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var response = await _resource.GetAsync(435L);
            
            Assert.NotNull(response.Id);
            Assert.Equal("My printer is on fire!", response.Subject);
            Assert.Equal("The smoke is very colorful.", response.Comment.Body);
        }

        [Fact]
        public async Task ShouldUpdateTicket()
        {
            var response = await _resource.PutAsync(
                new Contracts.Requests.TicketRequest
                {
                    Item = new Contracts.Models.Ticket
                    {
                        Id = 491,
                        Subject = "My printer is no longer on fire!",
                        Comment = new Contracts.Models.TicketComment
                        {
                            Body = "The smoke is gone."
                        }
                    }
                });
            
            Assert.NotNull(response.Id);
            Assert.Equal("My printer is no longer on fire!", response.Subject);
            Assert.Equal("The smoke is gone.", response.Comment.Body);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
