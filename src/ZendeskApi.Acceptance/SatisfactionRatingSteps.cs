using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class SatisfactionRatingSteps
    {
        private IZendeskClient _customerClient;
        private IZendeskClient _supportUserClient;
        private Ticket _ticket;
        private SatisfactionRating _satisfactionRating;
        private SatisfactionRating _savedSatisfactionRating;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _customerClient = new ZendeskClient(Host, new ZendeskDefaultConfiguration(Enduser, Token));

            _supportUserClient = new ZendeskClient(Host, new ZendeskDefaultConfiguration(Username, Token));
        }

        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescription(string subject, string description)
        {
            var query = new ZendeskQuery<User>().WithCustomFilter("email",
                ConfigurationManager.AppSettings["zendeskenduserusername"], FilterOperator.Equals);
            var requesterId = _supportUserClient.Search.Find(query).Results.First().Id;

            var ticket = new Ticket
            {
                Subject = subject,
                Comment = new TicketComment {Body = description},
                RequesterId = requesterId,
                Status = TicketStatus.Solved,
                Type = TicketType.question
            };
            var ticketRequest = new TicketRequest {Item = ticket};
            _ticket = _supportUserClient.Tickets.Post(ticketRequest).Item;

            _ticket.Status = TicketStatus.Solved;

            var ticketSolved = new TicketRequest {Item = _ticket};
            _ticket = _supportUserClient.Tickets.Put(ticketSolved).Item;
        }


        [Given(@"a satisfaction rating with the score '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(SatisfactionRatingScore score)
        {
            if (_ticket == null || !_ticket.Id.HasValue)
            {
                throw new NullReferenceException("Ticket (or ticket ID) cannot be null");
            }
            var rating = new SatisfactionRating {Score = score};
            var request = new SatisfactionRatingRequest {Item = rating};
            _satisfactionRating = _customerClient.SatisfactionRating.Post(request, _ticket.Id.Value).Item;
        }


        [When(@"I call get satisfaction rating by id on the ZendeskApiClient")]
        public void WhenIGetSatisfactionRating()
        {
            if (_satisfactionRating == null || !_satisfactionRating.Id.HasValue)
            {
                throw new NullReferenceException("Satisfaction rating (or its ID) cannot be null");
            }
            _savedSatisfactionRating = _supportUserClient.SatisfactionRating.Get(_satisfactionRating.Id.Value).Item;
        }

        [Then(@"I get a satisfaction rating with a score of (.*)")]
        public void ThenIGetASatisfactionRatingWithAScoreOf(SatisfactionRatingScore score)
        {
            Assert.That(_savedSatisfactionRating.Score, Is.EqualTo(score));
        }

        private Uri Host
        {
            get { return new Uri(GetSetting("zendeskhost")); }
        }

        private string Enduser
        {
            get { return GetSetting("zendeskenduserusername"); }
        }

        private string Token
        {
            get { return GetSetting("zendesktoken"); }
        }

        private string Username
        {
            get { return GetSetting("zendeskusername"); }
        }

        private string GetSetting(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(setting))
            {
                Assert.Fail("Setting '{0}' must be set", key);
            }
            return setting;
        }
    }
}
