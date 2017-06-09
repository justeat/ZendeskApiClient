using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketFormsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketFormsResource _resource;

        public TicketFormsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new TicketFormsResourceSampleSite(resource));
            _resource = new TicketFormsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllTicketForms()
        {
            var obj1 = await _resource.PostAsync(new TicketForm {
                Name = "Superman1"
            });

            var obj2 = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman2"
            });

            var objs = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[1]));
        }
        
        [Fact]
        public async Task ShouldGetTicketFormById()
        {
            var obj1 = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman1"
            });

            var obj2 = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman2"
            });

            var objs1 = await _resource.GetAsync(obj1.Id.Value);
            var objs2 = await _resource.GetAsync(obj2.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs1));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs2));
        }

        [Fact]
        public async Task ShouldCreateTicketForm()
        {
            var obj1 = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman1"
            });

            Assert.NotNull(obj1.Id);
            Assert.Equal("Superman1", obj1.Name);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201()
        {
            return Assert.ThrowsAsync<HttpRequestException>(async () => await _resource.PostAsync(new TicketForm
            {
                Name = "error"
            }));

            // could use tags to simulate httpstatus codes in fake client?
        }

        [Fact]
        public async Task ShouldUpdateTicketForm()
        {
            var obj = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman1"
            });

            Assert.Equal("Superman1", obj.Name);

            obj.Name = "Superman2";

            obj = await _resource.PutAsync(obj);

            Assert.Equal("Superman2", obj.Name);
        }

        [Fact]
        public async Task ShouldDeleteTicketForm()
        {
            var obj = await _resource.PostAsync(new TicketForm
            {
                Name = "Superman1"
            });

            var obj1 = await _resource.GetAsync(obj.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj), JsonConvert.SerializeObject(obj1));

            await _resource.DeleteAsync(obj.Id.Value);

            var obj2 = await _resource.GetAsync(obj.Id.Value);

            Assert.Null(obj2);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
