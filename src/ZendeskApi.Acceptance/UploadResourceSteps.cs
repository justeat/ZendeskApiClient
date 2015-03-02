using System;
using System.Configuration;
using System.IO;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ZendeskApi.Acceptance.Helpers;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Acceptance
{
    namespace JustEat.ZendeskApi.Acceptance
    {
        [Binding]
        public class UploadResourceSteps
        {
            private ZendeskClient _client;
            private string _filePath;
            private IResponse<Upload> _response;
            private static string _uploadToken;
            private bool _okResponse;

            [BeforeScenario]
            public void BeforeScenario()
            {
                _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                    new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"],
                        ConfigurationManager.AppSettings["zendesktoken"]));
            }

            [Given(@"I have a file to upload")]
            public void GivenIHaveAFileToUpload()
            {
                _uploadToken = string.Empty;

                var fileExists = false;
                var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
                if (directoryInfo != null)
                {
                    _filePath = Path.Combine(directoryInfo.FullName, "Picture.jpg");
                    if (File.Exists(_filePath))
                    {
                        fileExists = true;
                    }
                }
                Assert.That(fileExists);
            }

            [When(@"I call UploadResource Post")]
            public void WhenICallUploadResourcePost()
            {
                var fileInfo = new FileInfo(_filePath);
                var file = new MemoryFile(File.Open(_filePath, FileMode.Open), "image/jpeg", fileInfo.Name);
                _response = _client.Upload.Post(new UploadRequest
                {
                    Item = file
                });
            }

            [Then(@"I should get a token back")]
            public void ThenIShouldGetATokenBack()
            {
                Assert.That(!string.IsNullOrWhiteSpace(_response.Item.Token));
                _uploadToken = _response.Item.Token;
            }

            [Given(@"I have a valid token")]
            public void GivenIHaveAValidToken()
            {
                Assert.That(!string.IsNullOrWhiteSpace(_uploadToken));
            }

            [When(@"I delete an uploaded resource")]
            public void WhenICallUploadResourceDelete()
            {
                try
                {
                    _client.Upload.Delete(_uploadToken);
                    _okResponse = true;
                }
                catch (Exception e)
                {
                    _okResponse = false;
                }
            }

            [Then(@"I should get an OK response")]
            public void ThenIShouldGetOKResponse()
            {
                Assert.That(_okResponse);
            }

        }
    }
}
