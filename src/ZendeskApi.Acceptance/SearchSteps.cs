using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class SearchSteps
    {
        private IZendeskClient _client;

        private Organization _organization;
        private Organization _createdOrganization;

        private List<Organization> _searchResultsOne = new List<Organization>();
        private List<Organization> _searchResultsTwo = new List<Organization>();

        private string _usersEmail;
        private User _user;
            
        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"an organization in Zendesk named '(.*)'")]
        public void GivenAnOrganizationInZendeskWithTheNameAndTheCustomFieldAndValue(string name)
        {
            _createdOrganization = new Organization
            {
                Name = name + Guid.NewGuid(),
                Details = name,
                Notes = "new customer",
                Created = DateTime.UtcNow
            };

            _createdOrganization = _client.Organizations.Post(new OrganizationRequest { Item = _createdOrganization }).Item;
        }

        [Given(@"an organization in Zendesk with the name '(.*)' and the custom field '(.*)' and value '(.*)'")]
        public void GivenAnOrganizationInZendeskWithTheNameAndTheCustomFieldAndValue(string name, string customField, string value)
        {
            _createdOrganization = new Organization
            {
                Name = name + Guid.NewGuid(),
                CustomFields = new Dictionary<object, object> { {customField, value}},
                Details = name,
                Notes = "new customer"
            };

            _createdOrganization = _client.Organizations.Post(new OrganizationRequest { Item = _createdOrganization }).Item;
        }

        [Given(@"the email address of a user for whom I wish to search")]
        public void GivenTheEmailAddressOfAUserForWhomIWishToSearch()
        {
            _usersEmail = ConfigurationManager.AppSettings["zendeskusername"];
        }

        [When(@"I search for a user by their email address")]
        public void WhenISearchForAUserByTheirEmailAddress()
        {
            var response = _client.Search.Find(new ZendeskQuery<User>().WithCustomFilter("email", _usersEmail, FilterOperator.Equals));

            _user = response.Results.Single();
        }

        [Then(@"I am returned the correct user")]
        public void ThenIAmReturnedTheCorrectUser()
        {
            Assert.That(_user.Email, Is.EqualTo(_usersEmail));
        }

        [When(@"I search for organizations with the custom field '(.*)' and value '(.*)'")]
        public void WhenISearchForOrganizationsWithTheCustomFieldAndValue(string field, string value)
        {
            var searchResults = _client.Search.Find(new ZendeskQuery<Organization>().WithCustomFilter(field, value, FilterOperator.Equals));
            _organization = searchResults.Results.First();
        }

        [When(@"I search for the second organization by name")]
        public void WhenISearchForOrganizationsByTheName()
        {
            var searchResults = WaitForOrganizationToBeAvailiable(new ZendeskQuery<Organization>().WithCustomFilter("name", _createdOrganization.Name, FilterOperator.Equals));
            
            _organization = searchResults.Results.First();
        }

        [When(@"I search for organizations with the page size '(.*)' and page number '(.*)'")]
        public void WhenISearchForOrganizationsWithThePageSizeAndNumber(int  page, int pageNumber)
        {
            var searchResults =
                WaitForOrganizationToBeAvailiable(new ZendeskQuery<Organization>().WithPaging(pageNumber, page));
            _searchResultsOne = searchResults.Results.ToList();
        }

        [When(@"I search for organisations created today")]
        public void WhenISearchForOrganisationsCreatedToday()
        {
            var response = WaitForOrganizationToBeAvailiable(new ZendeskQuery<Organization>().WithCustomFilter("created", 
                DateTime.UtcNow.AddDays(-1).Date.ToString("yyyy-MM-dd"), FilterOperator.GreaterThan));

            _searchResultsOne = response.Results.ToList();
        }

        [When(@"I search again for organizations with the page size '(.*)' and page number '(.*)'")]
        public void WhenISearchAgainForOrganizationsWithThePageSizeAndNumber(int page, int pageNumber)
        {
            var searchResults = _client.Search.Find(new ZendeskQuery<Organization>().WithPaging(pageNumber, page));
            _searchResultsTwo = searchResults.Results.ToList();
        }

        [Then(@"I am returned differnt results containing '(.*)' item each")]
        public void ThenIAmReturnedResultsContainingXItems(int searchCount)
        {
            Assert.That(_searchResultsOne.Count, Is.EqualTo(searchCount));
            Assert.That(_searchResultsTwo.Count, Is.EqualTo(searchCount));
            Assert.That(_searchResultsOne.Any(o =>  _searchResultsTwo.Any(x => o.Name == x.Name)), Is.False);
        }

        [Then(@"I am returned the organization '(.*)'")]
        public void ThenIAmReturnedTheOrganization(string name)
        {
            Assert.That(_organization.Name, Is.StringStarting(name));
        }

        [Then(@"I am returned only organisations with a created date of today")]
        public void ThenIAmReturnedThatUser()
        {
            Assert.That(_searchResultsOne.Count(u => u.Created > DateTime.UtcNow.Date), Is.GreaterThan(0));
            Assert.That(_searchResultsOne.Count(u => u.Created < DateTime.UtcNow.Date), Is.EqualTo(0));
        }


        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_createdOrganization != null)
                    _client.Organizations.Delete((long)_createdOrganization.Id);

                _searchResultsOne.ForEach(s => _client.Organizations.Delete((long)s.Id));
                _searchResultsTwo.ForEach(s => _client.Organizations.Delete((long)s.Id));
            }
            catch (HttpException)
            {

            }

        }

        private IListResponse<Organization> WaitForOrganizationToBeAvailiable(IZendeskQuery<Organization> query)
        {
            IListResponse<Organization> searchResults = new ListResponse<Organization>() { Results = new List<Organization>() };

            var i = 100;

            while (i > 0 && !searchResults.Results.Any())
            {
                searchResults = _client.Search.Find(query);
                i--;
                Thread.Sleep(3000);
            }

            if (searchResults == null || searchResults.Results == null || !searchResults.Results.Any())
                Assert.Fail("Query returned no matching results");

            return searchResults;
        }
           

    }
}
