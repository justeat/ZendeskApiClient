using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Client.Serialization;
using Moq;
using NUnit.Framework;
using HttpRequest = JustEat.ZendeskApi.Client.Http.HttpRequest;
using HttpResponse = JustEat.ZendeskApi.Client.Http.HttpResponse;

namespace JustEat.ZendeskApi.Client.Tests
{
    public class ZendeskClientFixture
    {
        private Mock<ISerializer> _serializer;
        private IHttpResponse _successResponse;
        private IHttpResponse _failureResponse;

        [SetUp]
        public void SetUp()
        {
            _successResponse = new HttpResponse(true) { Content = "cheese" };
            _failureResponse = new HttpResponse(false) { Content = "error", StatusCode = HttpStatusCode.BadRequest };

            _serializer = new Mock<ISerializer>();
            _serializer.Setup(s => s.Deserialize<string>(_successResponse.Content))
                .Returns("cheese");
        }

        [Test]
        public void Get_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Get(It.IsAny<IHttpRequest>()))
                .Returns(_successResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.Get<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void GetAsync_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            var httpResponseTask = new TaskCompletionSource<IHttpResponse>();
            httpResponseTask.SetResult(_successResponse);
            http.Setup(h => h.GetAsync(It.IsAny<IHttpRequest>()))
                .Returns(httpResponseTask.Task);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.GetAsync<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result.Result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void Get_HttpReturnsFailResult_ThrowsException()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Get(It.IsAny<IHttpRequest>()))
                .Returns(_failureResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When, Then
            Assert.Throws<HttpException>(() => client.Get<string>(new Uri("http://someurl.co.uk/resource")));
        }

        [Test]
        public void Put_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Put(It.IsAny<IHttpRequest>()))
                .Returns(_successResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.Put<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void PutAsync_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            var httpResponseTask = new TaskCompletionSource<IHttpResponse>();
            httpResponseTask.SetResult(_successResponse);
            http.Setup(h => h.PutAsync(It.IsAny<IHttpRequest>()))
                .Returns(httpResponseTask.Task);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.PutAsync<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result.Result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void Put_HttpReturnsFailResult_ThrowsException()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Put(It.IsAny<IHttpRequest>()))
                .Returns(_failureResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When, Then
            Assert.Throws<HttpException>(() => client.Put<string>(new Uri("http://someurl.co.uk/resource")));
        }

        [Test]
        public void Post_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Post(It.IsAny<IHttpRequest>()))
                .Returns(_successResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.Post<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void PostAsync_Success_ReturnsSuccessResult()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            var httpResponseTask = new TaskCompletionSource<IHttpResponse>();
            httpResponseTask.SetResult(_successResponse);
            http.Setup(h => h.PostAsync(It.IsAny<IHttpRequest>()))
                .Returns(httpResponseTask.Task);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.PostAsync<string>(new Uri("http://someurl.co.uk/resource"));

            // Then
            Assert.That(result.Result, Is.EqualTo(_successResponse.Content));
        }

        [Test]
        public void Post_HttpReturnsFailResult_ThrowsException()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Post(It.IsAny<IHttpRequest>()))
                .Returns(_failureResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When, Then
            Assert.Throws<HttpException>(() => client.Post<string>(new Uri("http://someurl.co.uk/resource")));
        }

        [Test]
        public void Delete_Success_CallsDeleteOnHttp()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            http.Setup(h => h.Delete(It.IsAny<IHttpRequest>()))
                .Returns(_successResponse);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            client.Delete(new Uri("http://someurl.co.uk/resource"));

            // Then
            http.Verify(h => h.Delete(It.IsAny<HttpRequest>()));
        }

        [Test]
        public void DeleteAsync_Success_CallsDeleteOnHttp()
        {
            // Given
            var http = new Mock<IHttpChannel>();
            var httpResponseTask = new TaskCompletionSource<IHttpResponse>();
            httpResponseTask.SetResult(_successResponse);
            http.Setup(h => h.DeleteAsync(It.IsAny<IHttpRequest>()))
                .Returns(httpResponseTask.Task);

            var client = new ZendeskClient(new Uri("http://someurl.co.uk"), new ZendeskDefaultConfiguration("bob", "x1234//#"), _serializer.Object, http.Object);

            // When
            var result = client.DeleteAsync(new Uri("http://someurl.co.uk/resource"));

            // Then
            http.Verify(h => h.DeleteAsync(It.IsAny<HttpRequest>()));
            Assert.That(result.IsCompleted, Is.EqualTo(true));
        }
    }
}
