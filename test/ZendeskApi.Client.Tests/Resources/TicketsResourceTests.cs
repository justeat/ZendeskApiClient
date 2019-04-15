using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using System.Collections.Generic;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketsResource _resource;

        public TicketsResourceTests()
        {
            _client = new DisposableZendeskApiClient<TicketResourceState, Ticket>((resource) => new TicketResourceSampleSite(resource));
            _resource = new TicketsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllTickets()
        {
            var tickets = await CreateTickets(2);

            var retrievedTickets = (await _resource.ListAsync()).ToArray();

            Assert.Equal(2, retrievedTickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }

        [Fact]
        public async Task ShouldPaginateThoughTickets()
        {
            var tickets = await CreateTickets(10);

            var retrievedTickets = (await _resource.ListAsync(new PagerParameters { Page = 1, PageSize = 5 })).ToArray();

            Assert.Equal(5, retrievedTickets.Length);
            for (var i = 0; i < 5; i++)
            {
                Assert.Equal(JsonConvert.SerializeObject(tickets[i]), JsonConvert.SerializeObject(retrievedTickets[i]));
            }

            var retrievedTickets2 = (await _resource.ListAsync(new PagerParameters { Page = 2, PageSize = 5 })).ToArray();

            Assert.Equal(5, retrievedTickets2.Length);
            for (var i = 0; i < 5; i++)
            {
                Assert.Equal(JsonConvert.SerializeObject(tickets[i + 5]), JsonConvert.SerializeObject(retrievedTickets2[i]));
            }
        }

        [Fact]
        public async Task ShouldListAllForOrganizationTickets()
        {
            var ticket1 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                OrganisationId = 16230
            };

            var ticket2 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                }
            };

            var ticket3 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                OrganisationId = 16230
            };

            var ticketsCreated = await CreateTickets(ticket1, ticket2, ticket3);

            var tickets = (await _resource.ListForOrganizationAsync(16230L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[0]), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[2]), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldListAllForRequestedUserTickets()
        {
            var ticket1 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                RequesterId = 10000
            };

            var ticket2 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                },
                RequesterId = 10000
            };

            var ticket3 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                }
            };

            var ticketsCreated = await CreateTickets(ticket1, ticket2, ticket3);

            var tickets = (await _resource.ListRequestedByAsync(10000L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[0]), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[1]), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldListAllForCcdUserTickets()
        {
            var ticket1 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { }
            };

            var ticket2 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { 2293 }
            };

            var ticket3 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                CollaboratorIds = new System.Collections.Generic.List<long> { 2293 }
            };

            var ticketsCrearted = await CreateTickets(ticket1, ticket2, ticket3);

            var tickets = (await _resource.ListCcdAsync(2293L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticketsCrearted[1]), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticketsCrearted[2]), JsonConvert.SerializeObject(tickets[1]));
        }
        
        [Fact]
        public async Task ShouldListAllForAssignedForUserTickets()
        {
            var ticket1 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 1",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 1"
                },
                AssigneeId = 2233
            };

            var ticket2 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 2",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 2"
                }
            };

            var ticket3 = new TicketCreateRequest("description")
            {
                Subject = "My printer is on fire! 3",
                Comment = new TicketComment
                {
                    Body = "The smoke is very colorful. 3"
                },
                AssigneeId = 2233
            };

            var ticketsCreated = await CreateTickets(ticket1, ticket2, ticket3);

            var tickets = (await _resource.ListAssignedToAsync(2233L)).ToArray();

            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[0]), JsonConvert.SerializeObject(tickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(ticketsCreated[2]), JsonConvert.SerializeObject(tickets[1]));
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var ticket = (await CreateTickets(1)).First();

            var response = await _resource.GetAsync(ticket.Id);

            Assert.Equal(JsonConvert.SerializeObject(new TicketResponse{Ticket = ticket}), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task ShouldGetAllTicketsForIds()
        {
            var tickets = await CreateTickets(2);
            
            var retrievedTickets = (await _resource.GetAsync(new[] { tickets[0].Id, 543521L, tickets[1].Id, 123445L })).ToArray();
            
            Assert.Equal(2, tickets.Length);
            Assert.Equal(JsonConvert.SerializeObject(tickets[0]), JsonConvert.SerializeObject(retrievedTickets[0]));
            Assert.Equal(JsonConvert.SerializeObject(tickets[1]), JsonConvert.SerializeObject(retrievedTickets[1]));
        }

        [Fact]
        public async Task ShouldCreateTicket()
        {
            var response = await _resource.CreateAsync(
                new TicketCreateRequest("description")
                {
                    Subject = "My printer is on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful."
                    }
                });

            Assert.NotEqual(0L, response.Ticket.Id);
            Assert.Equal("My printer is on fire!", response.Ticket.Subject);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201()
        {
            return Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(
                new TicketCreateRequest("description")
                {
                    Subject = "My printer is no longer on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is gone."
                    },
                    Tags = new List<string> { "error" }
                }));

            // could use tags to simulate httpstatus codes in fake client?
        }
        
        [Fact]
        public async Task ShouldUpdateTicket()
        {
            var ticket = (await CreateTickets(1)).First();
            
            Assert.Equal("My printer is on fire! 0", ticket.Subject);

            var updateTicketRequest = new TicketUpdateRequest(ticket.Id)
            {
                Subject = "I COMMAND YOU TO UPDATE!!!"
            };

            ticket = (await _resource.UpdateAsync(updateTicketRequest)).Ticket;

            Assert.Equal("I COMMAND YOU TO UPDATE!!!", ticket.Subject);
        }
        
        [Fact]
        public async Task ShouldUpdateMultipleTickets()
        {
            var tickets = await CreateTickets(3);

            var updates = new List<TicketUpdateRequest>();
            var counter = 0;
            foreach (var ticket in tickets)
            {
                Assert.StartsWith("My printer is on fire! ", ticket.Subject);

                var updateTicketRequest = new TicketUpdateRequest(ticket.Id)
                {
                    Subject = "I COMMAND YOU TO UPDATE!!! " + counter++
                };

                updates.Add(updateTicketRequest);
            }

            var jobStatusResponse = await _resource.UpdateAsync(updates);

            Assert.NotNull(jobStatusResponse);
        }

        [Fact]
        public async Task ShouldDeleteTicket()
        {
            var ticket = (await CreateTickets(1)).First();

            var ticket1 = await _resource.GetAsync(ticket.Id);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(ticket1.Ticket));

            await _resource.DeleteAsync(ticket.Id);

            var ticket2 = await _resource.GetAsync(ticket.Id);

            Assert.Null(ticket2);
        }

        [Fact]
        public async Task ShouldDeleteMultipleTickets()
        {
            var tickets = await CreateTickets(3);
            var ticketIds = tickets.Select(t => t.Id).ToArray();

            await _resource.DeleteAsync(ticketIds);

            var doTicketsStillExistResult = await _resource.GetAsync(ticketIds);

            Assert.Empty(doTicketsStillExistResult);
        }

        private async Task<Ticket[]> CreateTickets(int numberOfTicketsToCreate)
        {
            var tickets = new List<TicketCreateRequest>();

            for (var i = 0; i < numberOfTicketsToCreate; i++)
            {
                var ticket = new TicketCreateRequest("Description is required")
                {
                    Subject = "My printer is on fire! " + i,
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful. " + i
                    }
                };

                tickets.Add(ticket);
            }

            return await CreateTickets(tickets.ToArray());
        }

        private async Task<Ticket[]> CreateTickets(params TicketCreateRequest[] tickets)
        {
            var createdTickets = new List<Ticket>();

            foreach (var ticketCreateRequest in tickets)
            {
                var response = await _resource.CreateAsync(ticketCreateRequest);
                createdTickets.Add(response.Ticket);
            }

            return createdTickets.ToArray();
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
