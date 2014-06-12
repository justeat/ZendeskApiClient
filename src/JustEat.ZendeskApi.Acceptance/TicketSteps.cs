using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using JustEat.ZendeskApi.Client;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
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
        public void GivenTheFollowingTicketsInZendesk(Table table)
        {
            var tickets = table.Rows.Select(row => new Ticket{ Subject = row["Subject"], Description = row["Description"]}).ToList();

            tickets.ForEach(t => _savedMultipleTicket.Add(_client.Ticket.Post(new TicketRequest { Ticket = t }).Ticket));
        }

        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedSingleTicket =
                _client.Ticket.Post(new TicketRequest
                {
                    Ticket = new Ticket {Subject = subject, Description = description}
                }).Ticket;
        }

        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleTicket.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleTicketResponse = _client.Ticket.Get(_savedSingleTicket.Id.Value).Ticket;
        }

        [When(@"I call getall by id on the ZendeskApiClient")]
        public void WhenICallGetallOnTheZendeskApiClient()
        {
            _multipleTicketResponse.AddRange(_client.Ticket.GetAll(_savedMultipleTicket.Select(t => t.Id.Value).ToList()).Results);
        }


        [Then(@"I get a ticket from Zendesk with the subject '(.*)' and description '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            Assert.That(_singleTicketResponse.Subject, Is.EqualTo(subject));
            Assert.That(_singleTicketResponse.Description, Is.EqualTo(description));
            Assert.That(_singleTicketResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get a ticket from Zendesk with the following values")]
        public void ThenIGetATicketFromZendeskWithTheFollowingValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.That(_multipleTicketResponse.Any(t => t.Subject.Equals(row["Subject"]) && t.Description.Equals(row["Description"])));
            }
        }


    }
}
