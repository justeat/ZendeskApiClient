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
            _customerClient = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskenduserusername"], ConfigurationManager.AppSettings["zendesktoken"]));

            _supportUserClient = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                 new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescription(string subject, string description)
        {
            var requesterId =
                _supportUserClient.Search.Find<User>(new ZendeskQuery<User>().WithCustomFilter("email",
                    ConfigurationManager.AppSettings["zendeskenduserusername"], FilterOperator.Equals)).Results.First().Id;

            var ticket = new Ticket
            {
                Subject = subject,
                Comment = new TicketComment {Body = description},
                RequesterId = requesterId,
                Type = TicketType.question,
                Status = TicketStatus.Solved
            };
            var ticketRequest = new TicketRequest {Item = ticket};

            _ticket = _supportUserClient.Tickets.Post(ticketRequest).Item;

            _ticket.Status = TicketStatus.Solved;

            _ticket = _supportUserClient.Tickets.Put(new TicketRequest
                {
                    Item = _ticket
                }
            ).Item;
        }


        [Given(@"a satisfaction rating with the score '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(SatisfactionRatingScore score)
        {
            _satisfactionRating =
                _customerClient.SatisfactionRating.Post(new SatisfactionRatingRequest
                {
                    Item = new SatisfactionRating {Score = score }
                }, _ticket.Id.Value).Item;
        }


        [When(@"I call get satisfaction rating by id on the ZendeskApiClient")]
        public void WhenIGetSatisfactionRating()
        {
            _savedSatisfactionRating = _supportUserClient.SatisfactionRating.Get((long)_satisfactionRating.Id).Item;
        }

        [Then(@"I get a satisfaction rating with a score of (.*)")]
        public void ThenIGetASatisfactionRatingWithAScoreOf(SatisfactionRatingScore score)
        {
            Assert.That(_savedSatisfactionRating.Score, Is.EqualTo(score));
        }
    }
}
