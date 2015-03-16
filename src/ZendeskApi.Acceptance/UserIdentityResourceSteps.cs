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
        private string _randomEmail;

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
                    Item = new User
                    {
                        Name = "TestUser-" + Guid.NewGuid(),
                        Email = string.Format("{0}@email.com", DateTime.Now.Ticks)
                    }
                }).Item;
            _randomEmail = string.Format("{0}@email.com", DateTime.UtcNow.Ticks);
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

            var oldId = identity.Id.Value;
            identity.Value = _randomEmail;
            identity.Id = null;

            _client.UserIdentities.Post(new UserIdentityRequest
            {
                Item = identity
            });

            _client.UserIdentities.Delete(oldId, identity.UserId.Value);
        }

        [Then(@"it should be changed")]
        public void ThenItShouldBeChanged()
        {
            _userIdentities = _client.UserIdentities.GetAll(_user.Id ?? 0);
            Assert.That(_userIdentities.Results.First().Value == _randomEmail);
        }


        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_user != null)
                {
                    _client.Users.Delete(_user.Id ?? 0);
                }
            }
            catch (HttpException) { }
        }
    }
}