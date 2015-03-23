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
    [Scope(Feature = "Requests")]
    [Binding]
    public class RequestSteps
    {
        private IZendeskClient _client;

        private readonly List<Request> _savedMultipleRequest = new List<Request>();
        private readonly List<Request> _multipleRequestResponse = new List<Request>();

        private int _customFieldId;

        private Request _savedSingleRequest;
        private Request _singleRequestResponse;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"the following requests in Zendesk")]
        [Given(@"I post the following requests")]
        public void GivenTheFollowingRequestsInZendesk(Table table)
        {
            var requests = table.Rows.Select(row => new Request { Subject = row["Subject"], Comment = new TicketComment { Body = row["Description"] }}).ToList();

            requests.ForEach(t => _savedMultipleRequest.Add(_client.Requests.Post(new RequestRequest { Item = t }).Item));
        }


        [Given(@"a request in Zendesk with the subject '(.*)' and comment '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string comment)
        {
            _savedSingleRequest =
                _client.Requests.Post(new RequestRequest
                {
                    Item = new Request { Subject = subject, Type = TicketType.task, Comment = new TicketComment { Body = comment } }
                }).Item;
        }


        [Given(@"a request in Zendesk with the subject '(.*)' and description '(.*)' and type '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionIWorkInTheseConditions(string subject, string description, string type)
        {
            _savedSingleRequest =
                _client.Requests.Post(new RequestRequest
                {
                    Item = new Request { Subject = subject, Comment = new TicketComment { Body = description }, Type = (TicketType)Enum.Parse(typeof(TicketType), type) }
                }).Item;
        }


        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleRequest.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleRequestResponse = _client.Requests.Get(_savedSingleRequest.Id.Value).Item;
        }

        [Then(@"I get a request from Zendesk with the subject '(.*)' and description '(.*)'")]
        public void ThenIGetARequestFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            Assert.That(_singleRequestResponse.Subject, Is.EqualTo(subject));
            Assert.That(_singleRequestResponse.Description, Is.EqualTo(description));
            Assert.That(_singleRequestResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get a request from Zendesk which is via the api")]
        public void ThenIGetARequestViaTheApi()
        {
            Assert.That(_singleRequestResponse.Via.Channel, Is.EqualTo("api"));
        }

        [When(@"I call delete by id on the ZendeskApiClient")]
        public void WhenICallDeleteByIdOnTheZendeskApiClient()
        {
            _client.Requests.Delete((long)_savedSingleRequest.Id);
        }

        [Then(@"the request is no longer in zendesk")]
        public void ThenTheRequestIsNoLongerInZendesk()
        {
            Assert.Throws<HttpException>(() => _client.Requests.Get((long) _savedSingleRequest.Id), "Requests not in Zendesk");
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedSingleRequest != null)
                    _client.Requests.Delete((long)_savedSingleRequest.Id);

                _savedMultipleRequest.ForEach(t => _client.Requests.Delete((long)t.Id));

            }
            catch (HttpException)
            {
                
            }
            
        }
        
    }
}
