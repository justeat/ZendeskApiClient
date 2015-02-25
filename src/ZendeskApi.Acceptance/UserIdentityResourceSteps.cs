using System;
using System.Configuration;
using System.Linq;
using System.Web;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class UserIdentityResourceSteps
    {
        private IZendeskClient _client;
        private User _user;
        private IListResponse<UserIdentity> _userIdentities;
        private readonly string newEmail = string.Format("someother{0}@email.com", DateTime.UtcNow.Date.Millisecond);

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"Zendesk User with an email")]
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

        [When(@"I change the email")]
        public void WhenIChangeTheEmail()
        {
            var identity = _userIdentities.Results.First();
            identity.Value = newEmail;

            _client.UserIdentities.Put(new UserIdentityRequest()
            {
                Item = identity
            });
        }

        [Then(@"it should be changed")]
        public void ThenItShouldBeChanged()
        {
            _userIdentities = _client.UserIdentities.GetAll(_user.Id ?? 0);
            Assert.That(_userIdentities.Results.First().Value == newEmail);
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
