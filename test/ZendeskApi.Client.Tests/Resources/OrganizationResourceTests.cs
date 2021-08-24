using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationResourceTests
    {
        private readonly OrganizationsResource _resource;

        public OrganizationResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<Organization>(resource => new OrganizationResourceSampleSite(resource));
            _resource = new OrganizationsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var org = results.ElementAt(i - 1);

                Assert.Equal($"org.{i}", org.Name);
                Assert.Equal(i.ToString(), org.ExternalId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync(new CursorPager{Size = 100});

            Assert.Equal(100, results.Count());

            for (var i = 1; i <= 100; i++)
            {
                var org = results.ElementAt(i - 1);

                Assert.Equal($"org.{i}", org.Name);
                Assert.Equal(i.ToString(), org.ExternalId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

            var org = results.First();

            Assert.Equal("org.2", org.Name);
            Assert.Equal("2", org.ExternalId);
        }

        [Fact]
        public async Task GetAllAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }));
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalled_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByUserIdAsync(1);

            Assert.Equal(1, results.Count);

            var org = results.First();

            Assert.Equal("org.1", org.Name);
            Assert.Equal("1", org.ExternalId);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAllByUserIdAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByUserIdAsync(int.MinValue));
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByUserIdAsync(
                1,
                new CursorPager
                {
                    Size = 1
                });

            Assert.Single(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByUserIdAsync(
                1, 
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOrganizationIds_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync(new long[] { 1, 2, 3 });

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var org = results.ElementAt(i - 1);

                Assert.Equal($"org.{i}", org.Name);
                Assert.Equal(i.ToString(), org.ExternalId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOrganizationIdsAndWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync(
                new long[] { 1, 2, 3 },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var org = results.First();

            Assert.Equal("org.2", org.Name);
            Assert.Equal("2", org.ExternalId);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOrganizationIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new long[] { long.MinValue }));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExternalIds_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByExternalIdsAsync(new string[] { "1", "2", "3" });

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var org = results.ElementAt(i - 1);

                Assert.Equal($"org.{i}", org.Name);
                Assert.Equal(i.ToString(), org.ExternalId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExternalIdsAndWithCursorPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByExternalIdsAsync(
                new [] { "1", "2", "3" },
                new CursorPager());

            var org = results.ElementAt(1);

            Assert.Equal("org.2", org.Name);
            Assert.Equal("2", org.ExternalId);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExternalIdsAndWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByExternalIdsAsync(
                new string[] { "1", "2", "3" },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var org = results.First();

            Assert.Equal("org.2", org.Name);
            Assert.Equal("2", org.ExternalId);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExternalIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByExternalIdsAsync(new string[] { long.MinValue.ToString() }));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGetOrganization()
        {
            var org = await _resource.GetAsync(1);

            Assert.Equal("org.1", org.Name);
            Assert.Equal("1", org.ExternalId);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreateOrganization()
        {
            var org = await _resource.CreateAsync(new Organization
            {
                Name = "OrgName",
                ExternalId = "123"
            });

            Assert.Equal("OrgName", org.Name);
            Assert.Equal("123", org.ExternalId);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new Organization
            {
                Name = string.Empty
            }));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldCreateOrganization()
        {
            var org = await _resource.UpdateAsync(new Organization
            {
                Id = 1,
                Name = "MyNewOrgName",
                ExternalId = "999999"
            });

            Assert.Equal("MyNewOrgName", org.Name);
            Assert.Equal("999999", org.ExternalId);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new Organization
            {
                Id = int.MaxValue,
                Name = "MyNewOrgName",
                ExternalId = "999999"
            });

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new Organization
            {
                Id = int.MinValue,
                Name = string.Empty
            }));
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateMultiple()
        {
            var orgs = new List<Organization>
            {
                new Organization {Id = 1},
                new Organization {Id = 2},
                new Organization {Id = 3}
            };
            
            var response = await _resource.UpdateAsync(orgs);
            Assert.NotNull(response);
            Assert.NotNull(response.Id);
        }

        [Fact]
        public async Task UpdateAsync_WhenMultiple_And_UnexpectedHttpCode_ShouldThrow()
        {
            var orgs = new List<Organization>
            {
                new Organization {Id = long.MinValue},
                new Organization {Id = 2},
            };
            
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(orgs));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDeleteOrganization()
        {
            await _resource.DeleteAsync(1);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue));
        }
    }
}
