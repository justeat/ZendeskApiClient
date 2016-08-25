using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentAssertions;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class TicketSteps
    {
        private IZendeskClient _client;

        private readonly List<Ticket> _savedMultipleTicket = new List<Ticket>();
        private readonly List<Ticket> _multipleTicketResponse = new List<Ticket>();

        private long _customFieldId;

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

            tickets.ForEach(t => _savedMultipleTicket.Add(_client.Tickets.Post(new TicketRequest { Item = t }).Item));
        }


        [Scope(Feature = "Tickets")]
        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedSingleTicket =
                _client.Tickets.Post(new TicketRequest
                {
                    Item = new Ticket {Subject = subject, Comment = new TicketComment { Body = description}, Type = TicketType.task}
                }).Item;
        }


        [Given(@"a task in Zendesk with the subject '(.*)' and description '(.*)' and type '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description, string type)
        {
            _savedSingleTicket =
                _client.Tickets.Post(new TicketRequest
                {
                    Item = new Ticket { Subject = subject, Description = description, Type = (TicketType)Enum.Parse(typeof(TicketType), type)}
                }).Item;
        }

        [Scope(Feature = "Tickets")]
        [When(@"I set the first ticket custom fields with the value of '(.*)'")]
        public void GivenTheTicketHasTheCustomFieldsAnd(string value)
        {
            _customFieldId = _savedSingleTicket.CustomFields.First().Id;

            _savedSingleTicket.CustomFields.First(c => c.Id == _customFieldId).Value = value;

            _client.Tickets.Put(new TicketRequest { Item = _savedSingleTicket });
        }

        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleTicket.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleTicketResponse = _client.Tickets.Get(_savedSingleTicket.Id.Value).Item;
        }

        [Scope(Feature = "Tickets")]
        [When(@"I call getall by id on the ZendeskApiClient")]
        public void WhenICallGetallOnTheZendeskApiClient()
        {
            _multipleTicketResponse.AddRange(_client.Tickets.GetAll(_savedMultipleTicket.Select(t => t.Id.Value).ToList()).Results);
        }

        [When(@"I update the ticket with the status '(.*)'")]
        public void WhenIUpdateTheTicketWithTheSubjectAndDescriptionTWorkInTheseConditions(TicketStatus status)
        {
            _savedSingleTicket.Status = status;

            _client.Tickets.Put(new TicketRequest { Item = _savedSingleTicket });
        }

        [Then(@"I get a ticket from Zendesk with the subject '(.*)' and description '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            Assert.That(_singleTicketResponse.Subject, Is.EqualTo(subject));
            Assert.That(_singleTicketResponse.Description, Is.EqualTo(description));
            Assert.That(_singleTicketResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get a task from Zendesk with the subject '(.*)' and description '(.*)' and type '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description, string type)
        {
            Assert.That(_singleTicketResponse.Subject, Is.EqualTo(subject));
            Assert.That(_singleTicketResponse.Description, Is.EqualTo(description));
            Assert.That(_singleTicketResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
            Assert.That(_singleTicketResponse.Type, Is.EqualTo((TicketType)Enum.Parse(typeof(TicketType), type)));
        }

        [Then(@"I get a ticket from Zendesk with the status '(.*)'")]
        public void ThenIGetATicketFromZendeskWithTheStatus(TicketStatus status)
        {
            Assert.That(_singleTicketResponse.Status, Is.EqualTo(status));
        }

        [Then(@"I get a ticket from Zendesk which is via the api")]
        public void ThenIGetATicketViaTheApi()
        {
            Assert.That(_singleTicketResponse.Via.Channel, Is.EqualTo("api"));
        }


        [Then(@"I get a ticket from Zendesk with the following values")]
        public void ThenIGetATicketFromZendeskWithTheFollowingValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.That(_multipleTicketResponse.Any(t => t.Subject.Equals(row["Subject"]) && t.Description.Equals(row["Description"])));
            }
        }

        [Then(@"the ticket has the custom field set with the value '(.*)'")]
        public void ThenTheTicketHasTheCustomFieldIdAndValue(string value)
        {
            Assert.That(_singleTicketResponse.CustomFields.First(c => c.Id == _customFieldId).Value, Is.EqualTo(value));
        }

        [When(@"I call delete by id on the ZendeskApiClient")]
        public void WhenICallDeleteByIdOnTheZendeskApiClient()
        {
            _client.Tickets.Delete((long)_savedSingleTicket.Id);
        }

        [Then(@"the ticket is no longer in zendesk")]
        public void ThenTheTicketIsNoLongerInZendesk()
        {
            //Assert.Throws<HttpException>(() => _client.Tickets.Get((long) _savedSingleTicket.Id), "Tickets not in Zendesk");
            Action act = () => _client.Tickets.Get((long) _savedSingleTicket.Id);
            act.ShouldThrow<AggregateException>().WithInnerMessage("{\"error\":\"RecordNotFound\",\"description\":\"Not found\"}");
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedSingleTicket != null)
                    _client.Tickets.Delete((long)_savedSingleTicket.Id);

                _savedMultipleTicket.ForEach(t => _client.Tickets.Delete((long)t.Id));

            }
            catch{}
        }
        
    }
}
