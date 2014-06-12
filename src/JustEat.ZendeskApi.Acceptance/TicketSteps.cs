using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JustEat.ZendeskApi.Client;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace JustEat.ZendeskApi.Acceptance
{
    [Binding]
    public class TicketSteps
    {
        private IZendeskClient _client;

        private readonly List<Ticket> _savedMultipleTicket = new List<Ticket>();
        private readonly List<Ticket> _multipleTicketResponse = new List<Ticket>();

        private Ticket _savedSingleTicket;
        private Ticket _singleTicketResponse;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"the following tickets in Zendesk")]
        [Given(@"I post the following tickets")]
        public void GivenTheFollowingTicketsInZendesk(Table table)
        {
            var tickets = table.Rows.Select(row => new Ticket{ Subject = row["Subject"], Description = row["Description"]}).ToList();

            tickets.ForEach(t => _savedMultipleTicket.Add(_client.Ticket.Post(new TicketRequest { Item = t }).Item));
        }

        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedSingleTicket =
                _client.Ticket.Post(new TicketRequest
                {
                    Item = new Ticket {Subject = subject, Description = description}
                }).Item;
        }

        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleTicket.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleTicketResponse = _client.Ticket.Get(_savedSingleTicket.Id.Value).Item;
        }

        [Scope(Feature = "Tickets")]
        [When(@"I call getall by id on the ZendeskApiClient")]
        public void WhenICallGetallOnTheZendeskApiClient()
        {
            _multipleTicketResponse.AddRange(_client.Ticket.GetAll(_savedMultipleTicket.Select(t => t.Id.Value).ToList()).Results);
        }

        [When(@"I update the ticket with the status '(.*)'")]
        public void WhenIUpdateTheTicketWithTheSubjectAndDescriptionTWorkInTheseConditions(TicketStatus status)
        {
            _savedSingleTicket.Status = status;

            _client.Ticket.Put(new TicketRequest { Item = _savedSingleTicket });
        }

        [Then(@"I get a ticket from Zendesk with the subject '(.*)' and description '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            Assert.That(_singleTicketResponse.Subject, Is.EqualTo(subject));
            Assert.That(_singleTicketResponse.Description, Is.EqualTo(description));
            Assert.That(_singleTicketResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get a ticket from Zendesk with the status '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheStatus(TicketStatus status)
        {
            Assert.That(_singleTicketResponse.Status, Is.EqualTo(status));
        }


        [Then(@"I get a ticket from Zendesk with the following values")]
        public void ThenIGetATicketFromZendeskWithTheFollowingValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.That(_multipleTicketResponse.Any(t => t.Subject.Equals(row["Subject"]) && t.Description.Equals(row["Description"])));
            }
        }

        [When(@"I call delete by id on the ZendeskApiClient")]
        public void WhenICallDeleteByIdOnTheZendeskApiClient()
        {
            _client.Ticket.Delete((long)_savedSingleTicket.Id);
        }

        [Then(@"the ticket is no longer in zendesk")]
        public void ThenTheTicketIsNoLongerInZendesk()
        {
            Assert.Throws<HttpException>(() => _client.Ticket.Get((long) _savedSingleTicket.Id), "Ticket not in Zendesk");
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedSingleTicket != null)
                    _client.Ticket.Delete((long)_savedSingleTicket.Id);

                _savedMultipleTicket.ForEach(t => _client.Ticket.Delete((long)t.Id));

            }
            catch (HttpException)
            {
                
            }
            
        }
        
    }
}
