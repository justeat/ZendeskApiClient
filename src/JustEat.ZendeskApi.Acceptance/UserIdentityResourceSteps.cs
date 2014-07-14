using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using JustEat.ZendeskApi.Client;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace JustEat.ZendeskApi.Acceptance
{
    [Binding]
    public class UserIdentityResourceSteps
    {
        private IZendeskClient _client;
        private User _user;
        private IListResponse<UserIdentity> _userIdentities;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"Zendesk User")]
        public void GivenZendeskUser()
        {
            _user =
                _client.Users.Post(new UserRequest
                {
                    Item = new User { 
                        Name = "TestUser-" + Guid.NewGuid(),
                        Email = string.Format("{0}@email.com", DateTime.Now.Ticks)
                    }
                }).Item;
        }

        [When(@"I call UserIdentityResource GetAll for this User")]
        public void WhenICallUserIdentityResourceGetAllForThisUser()
        {
            _userIdentities = _client.UserIdentities.GetAll(_user.Id ?? 0);
        }

        [Then(@"I should get list of user identities")]
        public void ThenIShouldGetListOfUserIdentities()
        {
            Assert.That(_userIdentities.Results.Any(i => i.Value == _user.Email));
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_user != null)
                {
                    _client.Users.Delete(_user.Id??0);
                }
            }
            catch (HttpException)  {}
        }
    }
}
