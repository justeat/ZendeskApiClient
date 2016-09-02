using System;
using System.Configuration;
using System.Web;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ZendeskApi.Acceptance
{
    [Scope(Feature = "Requests")]
    [Binding]
    public class RequestSteps
    {
        private IZendeskClient _client;

        private Request _savedSingleRequest;
        private Request _singleRequestResponse;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"a request in Zendesk with the subject '(.*)' and comment '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string comment)
        {
            _savedSingleRequest =
                _client.Request.Post(new RequestRequest
                {
                    Item = new Request { Subject = subject, Type = TicketType.task, Comment = new TicketComment { Body = comment } }
                }).Item;
        }


        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleRequest.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleRequestResponse = _client.Request.Get(_savedSingleRequest.Id.Value).Item;
        }

        [Then(@"I get a request from Zendesk with the subject '(.*)' and description '(.*)'")]
        public void ThenIGetARequestFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            StringAssert.AreEqualIgnoringCase(subject, _singleRequestResponse.Subject);
            StringAssert.StartsWith(description, _singleRequestResponse.Description);
            Assert.That(_singleRequestResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get a request from Zendesk which is via the api")]
        public void ThenIGetARequestViaTheApi()
        {
            Assert.That(_singleRequestResponse.Via.Channel, Is.EqualTo("api"));
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedSingleRequest != null)
                    _client.Tickets.Delete((long)_savedSingleRequest.Id);

            }
            catch (HttpException)
            {

            }

        }
    }
}
