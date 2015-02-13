using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class TicketCommentSteps
    {
        private IZendeskClient _client;
        private Ticket _savedTicket;
        private readonly List<TicketComment> _savedTicketComments = new List<TicketComment>();

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Scope(Feature = "TicketComments")]
        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedTicket =
                _client.Tickets.Post(new TicketRequest
                {
                    Item = new Ticket { Subject = subject, Comment = new TicketComment{ Body = description}, Type = TicketType.task }
                }).Item;
        }

        [Given(@"I add the comment '(.*)'")]
        public void WhenIAddTheComment(string comment)
        {
            _savedTicket.Comment = new TicketComment { Body = comment };

            _client.Tickets.Put(new TicketRequest { Item = _savedTicket });
        }

        [When(@"I call get all comments for that ticket")]
        public void WhenICallGetAllCommentsForThatTicket()
        {
            var allComments = _client.TicketComments.GetAll(_savedTicket.Id.Value);
            _savedTicketComments.AddRange(allComments.Results);
        }

        [Then(@"I am returned a comment with the body '(.*)'")]
        public void ThenIAmReturnedACommentWithTheBody(string comment)
        {
            Assert.That(_savedTicketComments.Any(c => c.Body.Contains(comment)));
        }
    }
}
