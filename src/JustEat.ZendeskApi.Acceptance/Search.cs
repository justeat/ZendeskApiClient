using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using JustEat.ZendeskApi.Client;
using JustEat.ZendeskApi.Client.Factories;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using JustEat.ZendeskApi.Contracts.Requests;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace JustEat.ZendeskApi.Acceptance
{
    [Binding]
    public class Search
    {
        private IZendeskClient _client;

        private Organization _organization;
        private Organization _createdOrganization;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"an organization in Zendesk with the name '(.*)' and the custom field '(.*)' and value '(.*)'")]
        public void GivenAnOrganizationInZendeskWithTheNameAndTheCustomFieldAndValue(string name, string customField, string value)
        {
            _createdOrganization = new Organization
            {
                Name = name + Guid.NewGuid(),
                CustomFields = new Dictionary<object, object> { {customField, value}}
            };

            _createdOrganization = _client.Organizations.Post(new OrganizationRequest { Item = _createdOrganization }).Item;
        }

        [When(@"I search for organizations with the custom field '(.*)' and value '(.*)'")]
        public void WhenISearchForOrganizationsWithTheCustomFieldAndValue(string field, string value)
        {
            var searchResults = _client.Search.Get<Organization>(
                new QueryFactory(new TypeQuery
                {
                    CustomField = field,
                    Type = ZendeskType.Organization,
                    CustomFieldValue = value
                }));
            _organization = searchResults.Results.First();
        }

        [Then(@"I am returned the organization '(.*)'")]
        public void ThenIAmReturnedTheOrganization(string name)
        {
            Assert.That(_organization.Name, Is.StringStarting(name));
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_createdOrganization != null)
                    _client.Organizations.Delete((long)_createdOrganization.Id);
            }
            catch (HttpException)
            {

            }

        }

    }
}
