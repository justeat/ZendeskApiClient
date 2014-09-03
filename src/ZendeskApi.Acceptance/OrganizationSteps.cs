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
    public class OrganizationSteps
    {
        private IZendeskClient _client;

        private readonly List<Organization> _savedMultipleOrganizations = new List<Organization>();
        private readonly List<Organization> _multipleOrganizationResponse = new List<Organization>();

        private Organization _savedSingleOrganization;
        private Organization _singleOrganizationResponse;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"the following Organizations in Zendesk")]
        [Given(@"I post the following Organizations")]
        public void GivenTheFollowingOrganizationsInZendesk(Table table)
        {
            var Organizations = table.Rows.Select(row => new Organization { Name = row["Name"] + Guid.NewGuid() }).ToList();

            Organizations.ForEach(t => _savedMultipleOrganizations.Add(_client.Organizations.Post(new OrganizationRequest { Item = t }).Item));
        }

        [Scope(Feature = "Organization")]
        [Given(@"an organization in Zendesk with the name '(.*)'")]
        public void GivenAOrganizationInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string name)
        {
            _savedSingleOrganization =
                _client.Organizations.Post(new OrganizationRequest
                {
                    Item = new Organization { Name = name + Guid.NewGuid() }
                }).Item;
        }

        [Scope(Feature = "Organization")]
        [When(@"I call get by id on the ZendeskApiClient")]
        public void WhenICallGetByIdOnTheZendeskApiClient()
        {
            if (!_savedSingleOrganization.Id.HasValue)
                throw new ArgumentException("Cannot get by id when id is null");

            _singleOrganizationResponse = _client.Organizations.Get(_savedSingleOrganization.Id.Value).Item;
        }

        [When(@"I update the organization with the name '(.*)'")]
        public void WhenIUpdateTheOrganizationWithTheSubjectAndDescriptionTWorkInTheseConditions(string name)
        {
            _savedSingleOrganization.Name = name + Guid.NewGuid();

            _client.Organizations.Put(new OrganizationRequest { Item = _savedSingleOrganization });
        }

        [Then(@"I get an Organization from Zendesk with the name '(.*)'")]
        public void ThenIGetAOrganizationFromZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string name)
        {
            Assert.That(_singleOrganizationResponse.Name, Is.StringStarting(name));
            Assert.That(_singleOrganizationResponse.Created, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-2)));
        }

        [Then(@"I get organisations from Zendesk with the following values")]
        public void ThenIGetAOrganizationFromZendeskWithTheFollowingValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.That(_multipleOrganizationResponse.Any(t => t.Name.StartsWith(row["Name"])));
            }
        }

        [Scope(Feature = "Organization")]
        [When(@"I call delete by id on the ZendeskApiClient")]
        public void WhenICallDeleteByIdOnTheZendeskApiClient()
        {
            _client.Organizations.Delete((long)_savedSingleOrganization.Id);
        }

        [Then(@"the Organization is no longer in zendesk")]
        public void ThenTheOrganizationIsNoLongerInZendesk()
        {
            Assert.Throws<HttpException>(() => _client.Organizations.Get((long)_savedSingleOrganization.Id), "Organization not in Zendesk");
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedSingleOrganization != null)
                    _client.Organizations.Delete((long)_savedSingleOrganization.Id);

                _savedMultipleOrganizations.ForEach(t => _client.Organizations.Delete((long)t.Id));

            }
            catch (HttpException)
            {

            }

        }

    }
}
