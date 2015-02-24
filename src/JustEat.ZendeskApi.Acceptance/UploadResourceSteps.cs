using System;
using System.Configuration;
using System.IO;
using JustEat.ZendeskApi.Acceptance.Helpers;
using JustEat.ZendeskApi.Client;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace JustEat.ZendeskApi.Acceptance
{
    [Binding]
    public class UploadResourceSteps
    {
        private ZendeskClient _client;
        private string _filePath;
        private IResponse<Upload> _response;
        private static string UploadToken = string.Empty;
        private bool OKResponse;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Given(@"I have a file to upload")]
        public void GivenIHaveAFileToUpload()
        {
            UploadToken = string.Empty;

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
            _response = _client.Uploads.Post(new UploadRequest
            {
                Item = file
            });
        }

        [Then(@"I should get a token back")]
        public void ThenIShouldGetATokenBack()
        {
            Assert.That(!string.IsNullOrWhiteSpace(_response.Item.Token));
            UploadToken = _response.Item.Token;
        }

        [Given(@"I have a valid token")]
        public void GivenIHaveAValidToken()
        {
            Assert.That(!string.IsNullOrWhiteSpace(UploadToken));
        }

        [When(@"I call UploadResource Delete")]
        public void WhenICallUploadResourceDelete()
        {
            try
            {
                _client.Uploads.Delete(UploadToken);
                OKResponse = true;
            }
            catch (Exception e)
            {
                OKResponse = false;
            }
        }

        [Then(@"I should get OK response")]
        public void ThenIShouldGetOKResponse()
        {
            Assert.That(OKResponse);
        }

    }
}
