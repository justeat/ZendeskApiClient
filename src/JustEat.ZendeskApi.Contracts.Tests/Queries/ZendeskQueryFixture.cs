using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Contracts.Tests.Queries
{
    public class ZendeskQueryFixture
    {
        [Test]
        public void CalledWithCustomFields_BuildsQuery()
        {
            // Given
            var query = new ZendeskZendeskQuery<Organization>();
            query.WithCustomFilter("name", "cheese factory");

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.That(queryString, Is.EqualTo("query=type:organization name:cheese factory&sort_by=created_at&sort_order=desc&page=1&per_page=15"));
        }

        [Test]
        public void CalledWithCustomFieldsAndPage_BuildsQuery()
        {
            // Given
            var query = new ZendeskZendeskQuery<Organization>();
            query.WithCustomFilter("name", "cheese factory").WithPaging(3, PageSize.Fifteen);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.That(queryString, Is.EqualTo("query=type:organization name:cheese factory&sort_by=created_at&sort_order=desc&page=3&per_page=15"));
        }

        [Test]
        public void CalledWithDifferentType_BuildsQuery()
        {
            // Given
            var query = new ZendeskZendeskQuery<Ticket>();
            query.WithCustomFilter("name", "cheese factory").WithPaging(3, PageSize.Fifteen);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.That(queryString, Is.EqualTo("query=type:ticket name:cheese factory&sort_by=created_at&sort_order=desc&page=3&per_page=15"));
        }

        [Test]
        public void CalledWithOrderSet_BuildsQuery()
        {
            // Given
            var query = new ZendeskZendeskQuery<Ticket>();
            query.WithCustomFilter("name", "cheese factory").WithPaging(3, PageSize.Fifteen).WithOrdering(OrderBy.priority, Order.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.That(queryString, Is.EqualTo("query=type:ticket name:cheese factory&sort_by=priority&sort_order=asc&page=3&per_page=15"));
        }
    }
}
