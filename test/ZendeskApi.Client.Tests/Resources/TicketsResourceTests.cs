using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketsResource _resource;

        public TicketsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new TicketResourceSampleSite(resource));
            _resource = new TicketsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllTickets()
        {
            var tickets = await CreateTickets();

            var retrievedTickets = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, retrievedTickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }
        
        [Fact]
        public async Task ShouldListAllForOrganizationTickets()
        {
            var ticket1 = new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                OrganisationId = 16230
            };

            var ticket2 = new Ticket
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                }
            };

            var ticket3 = new Ticket
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                OrganisationId = 16230
            };

            var jobStatus = await _resource.PostAsync(new[] { ticket1, ticket2, ticket3 });

            ticket1.Id = jobStatus.Items.Single(x => x.Title == ticket1.Subject).Id;
            ticket2.Id = jobStatus.Items.Single(x => x.Title == ticket2.Subject).Id;
            ticket3.Id = jobStatus.Items.Single(x => x.Title == ticket3.Subject).Id;

            var tickets = (await _resource.GetAllForOrganizationAsync(16230L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticket1), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticket3), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldListAllForRequestedUserTickets()
        {
            var ticket1 = new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                RequesterId = 10000
            };

            var ticket2 = new Ticket
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                },
                RequesterId = 10000
            };

            var ticket3 = new Ticket
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                }
            };

            var jobStatus = await _resource.PostAsync(new[] { ticket1, ticket2, ticket3 });

            ticket1.Id = jobStatus.Items.Single(x => x.Title == ticket1.Subject).Id;
            ticket2.Id = jobStatus.Items.Single(x => x.Title == ticket2.Subject).Id;
            ticket3.Id = jobStatus.Items.Single(x => x.Title == ticket3.Subject).Id;

            var tickets = (await _resource.GetAllRequestedForUserAsync(10000L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticket1), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticket2), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldListAllForCCDUserTickets()
        {
            var ticket1 = new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { }
            };

            var ticket2 = new Ticket
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { 2293 }
            };

            var ticket3 = new Ticket
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { 2293 }
            };

            var jobStatus = await _resource.PostAsync(new[] { ticket1, ticket2, ticket3 });

            ticket1.Id = jobStatus.Items.Single(x => x.Title == ticket1.Subject).Id;
            ticket2.Id = jobStatus.Items.Single(x => x.Title == ticket2.Subject).Id;
            ticket3.Id = jobStatus.Items.Single(x => x.Title == ticket3.Subject).Id;

            var tickets = (await _resource.GetAllCCDForUserAsync(2293L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticket2), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticket3), JsonConvert.SerializeObject(tickets[1]));
        }


        [Fact]
        public async Task ShouldListAllForAssignedForUserTickets()
        {
            var ticket1 = new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                AssigneeId = 2233
            };

            var ticket2 = new Ticket
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                }
            };

            var ticket3 = new Ticket
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                AssigneeId = 2233
            };

            var jobStatus = await _resource.PostAsync(new[] { ticket1, ticket2, ticket3 });

            ticket1.Id = jobStatus.Items.Single(x => x.Title == ticket1.Subject).Id;
            ticket2.Id = jobStatus.Items.Single(x => x.Title == ticket2.Subject).Id;
            ticket3.Id = jobStatus.Items.Single(x => x.Title == ticket3.Subject).Id;

            var tickets = (await _resource.GetAllAssignedForUserAsync(2233L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticket1), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticket3), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var ticket = await _resource.PostAsync(new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                }
            });

            var response = await _resource.GetAsync(ticket.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task ShouldGetAllTicketsForIds()
        {
            var tickets = await CreateTickets();
            
            var retrievedTickets = (await _resource.GetAllAsync(new long[] { tickets[0].Id.Value, 543521L, tickets[1].Id.Value, 123445L })).ToArray();
            
            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }

        [Fact]
        public async Task ShouldCreateTicket()
        {
            var response = await _resource.PostAsync(
                new Ticket
                {
                    Subject = "My printer is on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful."
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
                new Ticket
                {
                    Subject = "My printer is no longer on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is gone."
                    },
                    Tags = new System.Collections.Generic.List<string> { "error" }
                }));

            // could use tags to simulate httpstatus codes in fake client?
        }

        [Fact]
        public async Task ShouldJobWhenCreatingMultipleTicket()
        {
            var response = await _resource.PostAsync(
                new[] {
                        new Ticket {
                            Subject = "My printer is on fire!",
                            Comment = new TicketComment
                            {
                                Body = "The smoke is very colorful."
                            }
                        },
                        new Ticket {
                            Subject = "My printer is somehow on fire again!",
                            Comment = new TicketComment
                            {
                                Body = "The smoke is not very colorful."
                            }
                        }
                    });
            
            Assert.NotNull(response.Id);
        }
        
        [Fact]
        public async Task ShouldUpdateTicket()
        {
            var ticket = await _resource.PostAsync(
                new Ticket
                {
                    Subject = "My printer is on fire! 1",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful. 1"
                    }
                });

            Assert.Equal("My printer is on fire! 1", ticket.Subject);

            ticket.Subject = "I COMMAND YOU TO UPDATE!!!";

            ticket = await _resource.PutAsync(ticket);

            Assert.Equal("I COMMAND YOU TO UPDATE!!!", ticket.Subject);
        }

        [Fact]
        public async Task ShouldDeleteTicket()
        {
            var ticket = await _resource.PostAsync(
                new Ticket
                {
                    Subject = "My printer is on fire! 1",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful. 1"
                    }
                });

            var ticket1 = await _resource.GetAsync(ticket.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(ticket1));

            await _resource.DeleteAsync(ticket.Id.Value);

            var ticket2 = await _resource.GetAsync(ticket.Id.Value);

            Assert.Null(ticket2);
        }

        private async Task<Ticket[]> CreateTickets()
        {
            var ticket1 = new Ticket
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                }
            };

            var ticket2 = new Ticket
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                }
            };

            var jobStatus = await _resource.PostAsync(new[] { ticket1, ticket2 });

            ticket1.Id = jobStatus.Items.Single(x => x.Title == ticket1.Subject).Id;
            ticket2.Id = jobStatus.Items.Single(x => x.Title == ticket2.Subject).Id;

            return new[] { ticket1, ticket2 };
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
