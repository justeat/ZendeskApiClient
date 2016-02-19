using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ZendeskApi.Acceptance.Helpers;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ZendeskApi.Acceptance
{
    [Binding]
    public class TicketCommentSteps
    {
        private IZendeskClient _client;
        private Ticket _savedTicket;
        private Request _savedRequest;
        private string _uploadToken;
        private readonly List<TicketComment> _savedTicketComments = new List<TicketComment>();

        [BeforeScenario]
        public void BeforeScenario()
        {
            _client = new ZendeskClient(new Uri(ConfigurationManager.AppSettings["zendeskhost"]),
                new ZendeskDefaultConfiguration(ConfigurationManager.AppSettings["zendeskusername"], ConfigurationManager.AppSettings["zendesktoken"]));
        }

        [Scope(Feature = "TicketComments")]
        [Given(@"a ticket in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenATicketInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedTicket =
                _client.Tickets.Post(new TicketRequest
                {
                    Item = new Ticket { Subject = subject, Comment = new TicketComment { Body = description }, Type = TicketType.task }
                }).Item;
        }

        [Scope(Feature = "TicketComments")]
        [Given(@"a request in Zendesk with the subject '(.*)' and description '(.*)'")]
        public void GivenARequestInZendeskWithTheSubjectAndDescriptionTWorkInTheseConditions(string subject, string description)
        {
            _savedRequest =
                _client.Request.Post(new RequestRequest
                {
                    Item = new Request { Subject = subject, Comment = new TicketComment { Body = description }, Type = TicketType.task }
                }).Item;
        }

        [Given(@"I add the comment '(.*)'")]
        public void WhenIAddTheComment(string comment)
        {
            _savedTicket.Comment = new TicketComment { Body = comment };

            _client.Tickets.Put(new TicketRequest { Item = _savedTicket });
        }

        [Given(@"I add the comment '(.*)' to the request")]
        public void WhenIAddTheCommentToTheRequest(string comment)
        {
            _savedRequest.Comment = new TicketComment { Body = comment };

            _client.Request.Put(new RequestRequest { Item = _savedRequest });
        }

        [Given(@"I upload a file to attach to that comment")]
        public void GivenIHaveAFileToUpload()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null) throw new FileNotFoundException("Current directory has no parent");

            var filePath = Path.Combine(directoryInfo.FullName, "Picture.jpg");

            var fileInfo = new FileInfo(filePath);

            using (var file = new MemoryFile(File.Open(filePath, FileMode.Open), "image/jpeg", fileInfo.Name))
            {
                var response = _client.Upload.Post(new UploadRequest
                {
                    Item = file
                });

                _uploadToken = response.Item.Token;
            }
        }

        [When(@"I add the comment '(.*)' with the upload attached")]
        public void WhenIAddTheCommentWithTheUploadAttached(string comment)
        {
            _savedTicket.Comment = new TicketComment { Body = comment };

            _savedTicket.Comment.AddAttachmentToComment(_uploadToken);

            _client.Tickets.Put(new TicketRequest { Item = _savedTicket });
        }

        [When(@"I call get all comments for that ticket")]
        public void WhenICallGetAllCommentsForThatTicket()
        {
            if (_savedTicket == null || !_savedTicket.Id.HasValue) throw new NullReferenceException("No saved ticket");
            var allComments = _client.TicketComments.GetAll(_savedTicket.Id.Value);
            _savedTicketComments.AddRange(allComments.Results);
        }

        [When(@"I call get all comments for that request")]
        public void WhenICallGetAllCommentsForThatRequest()
        {
            if (_savedRequest == null || !_savedRequest.Id.HasValue) throw new NullReferenceException("No saved request");
            var allComments = _client.RequestComments.GetAll(_savedRequest.Id.Value);
            _savedTicketComments.AddRange(allComments.Results);
        }

        [Then(@"I am returned a comment with the body '(.*)'")]
        public void ThenIAmReturnedACommentWithTheBody(string comment)
        {
            Assert.That(_savedTicketComments.Any(c => c.Body.Contains(comment)));
        }

        [Then(@"I am returned a comment with the body '(.*)' with that attachment")]
        public void ThenIAmReturnedACommentWithTheBodyAndThatAttachment(string comment)
        {
            var postedComment = _savedTicketComments.First(c => c.Body.Contains(comment));

            Assert.That(postedComment.Attachments.Count == 1);
        }

        [Then(@"I am returned a comment that is made via the api")]
        public void ThenIAmReturnedACommentThatIsMadeViaTheApi()
        {
            Assert.That(_savedTicketComments.Any(c => c.Via.Channel == "api"));
        }

        [AfterScenario]
        public void AfterFeature()
        {
            try
            {
                if (_savedTicketComments != null && _savedTicketComments.Any())
                {
                    foreach (var comment in _savedTicketComments)
                    {
                        if (comment.Id.HasValue)
                        {
                            _client.Tickets.Delete(comment.Id.Value);
                        }
                    }
                }

                if (_savedRequest != null && _savedRequest.Id.HasValue)
                    _client.Tickets.Delete(_savedRequest.Id.Value);

                if (_savedTicket != null && _savedTicket.Id.HasValue)
                    _client.Tickets.Delete(_savedTicket.Id.Value);

            }
            catch (HttpException)
            {

            }

        }

    }
}