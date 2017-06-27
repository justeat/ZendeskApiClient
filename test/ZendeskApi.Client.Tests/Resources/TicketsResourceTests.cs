using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using System.Collections.Generic;

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
            var tickets = await CreateTickets(2);

            var retrievedTickets = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, retrievedTickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }

        [Fact]
        public async Task ShouldPaginateThoughTickets()
        {
            var tickets = await CreateTickets(10);

            var retrievedTickets = (await _resource.GetAllAsync(new PagerParameters { Page = 1, PageSize = 5 })).ToArray();

            Assert.Equal(5, retrievedTickets.Length);
            for (var i = 0; i < 5; i++)
            {
                Assert.Equal(JsonConvert.SerializeObject(tickets[i]), JsonConvert.SerializeObject(retrievedTickets[i]));
            }

            var retrievedTickets2 = (await _resource.GetAllAsync(new PagerParameters { Page = 2, PageSize = 5 })).ToArray();

            Assert.Equal(5, retrievedTickets2.Length);
            for (var i = 0; i < 5; i++)
            {
                Assert.Equal(JsonConvert.SerializeObject(tickets[i + 5]), JsonConvert.SerializeObject(retrievedTickets2[i]));
            }
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

            await CreateTickets(ticket1, ticket2, ticket3);

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

            await CreateTickets(ticket1, ticket2, ticket3);

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

            await CreateTickets(ticket1, ticket2, ticket3);

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

            await CreateTickets(ticket1, ticket2, ticket3);

            var tickets = (await _resource.GetAllAssignedForUserAsync(2233L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticket1), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticket3), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var ticket = (await CreateTickets(1)).First();

            var response = await _resource.GetAsync(ticket.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task ShouldGetAllTicketsForIds()
        {
            var tickets = await CreateTickets(2);
            
            var retrievedTickets = (await _resource.GetAllAsync(new long[] { tickets[0].Id.Value, 543521L, tickets[1].Id.Value, 123445L })).ToArray();
            
            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }

        [Fact]
        public async Task ShouldCreateTicket()
        {
            var response = await _resource.CreateAsync(
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
            return Assert.ThrowsAsync<HttpRequestException>(async () => await _resource.CreateAsync(
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
            var response = await _resource.CreateAsync(
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
            var ticket = (await CreateTickets(1)).First();

            Assert.Equal("My printer is on fire! 0", ticket.Subject);

            ticket.Subject = "I COMMAND YOU TO UPDATE!!!";

            ticket = await _resource.UpdateAsync(ticket);

            Assert.Equal("I COMMAND YOU TO UPDATE!!!", ticket.Subject);
        }

        [Fact]
        public async Task ShouldDeleteTicket()
        {
            var ticket = (await CreateTickets(1)).First();

            var ticket1 = await _resource.GetAsync(ticket.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(ticket1));

            await _resource.DeleteAsync(ticket.Id.Value);

            var ticket2 = await _resource.GetAsync(ticket.Id.Value);

            Assert.Null(ticket2);
        }

        private async Task<Ticket[]> CreateTickets(int numberOfTicketsToCreate)
        {
            var tickets = new List<Ticket>();

            for (var i = 0; i < numberOfTicketsToCreate; i++)
            {
                var ticket = new Ticket
                {
                    Subject = "My printer is on fire! " + i,
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful. " + i
                    }
                };

                tickets.Add(ticket);
            }

            var jobStatus = await _resource.CreateAsync(tickets);

            for (var i = 0; i < numberOfTicketsToCreate; i++)
            {
                tickets[i].Id = jobStatus.Items.Single(x => x.Title == tickets[i].Subject).Id;
                tickets[i].Url = new Uri("https://company.zendesk.com/api/v2/tickets/" + tickets[i].Id + ".json");
            }

            return tickets.ToArray();
        }

        private async Task<Ticket[]> CreateTickets(params Ticket[] tickets)
        {
            var jobStatus = await _resource.CreateAsync(tickets);

            for (var i = 0; i < tickets.Length; i++)
            {
                tickets[i].Id = jobStatus.Items.Single(x => x.Title == tickets[i].Subject).Id;
                tickets[i].Url = new Uri("https://company.zendesk.com/api/v2/tickets/" + tickets[i].Id + ".json");
            }

            return tickets.ToArray();
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
