using JustEat.ZendeskApi.Client.Factories;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Factories
{
    public class QueryFactoryFixture
    {
        [Test]
        public void CreateQuery_CalledWithListQueries_CreatesQuery()
        {
            // Given
            var order = new OrderQuery {Order = Order.Desc, OrderBy = OrderBy.created_at};
            var type = new TypeQuery {Type = ZendeskType.Ticket, CustomField = "field", CustomFieldValue = "value"};
            var page = new PagingQuery {PageNumber = 2, PageSize = 5};
            var factory = new QueryFactory(type, order, page);

            // When
            var result = factory.BuildQuery();

            // Then
            Assert.That(result, Is.EqualTo("query=type:ticket field:value&sort_by=created_at&sort_order=desc&page=2&per_page=5"));
        }

        [Test]
        public void CreateQuery_CalledWithPageOnly_CreatesQuery()
        {
            // Given
            var page = new PagingQuery { PageNumber = 2, PageSize = 5 };
            var factory = new QueryFactory(paging: page);

            // When
            var result = factory.BuildQuery();

            // Then
            Assert.That(result, Is.EqualTo("query=page=2&per_page=5"));
        }

        [Test]
        public void CreateQuery_CalledWithPageAndType_CreatesQuery()
        {
            // Given
            var page = new PagingQuery { PageNumber = 2, PageSize = 5 };
            var type = new TypeQuery { Type = ZendeskType.Ticket, CustomField = "field", CustomFieldValue = "value" };
            var factory = new QueryFactory(paging: page, type:type);

            // When
            var result = factory.BuildQuery();

            // Then
            Assert.That(result, Is.EqualTo("query=type:ticket field:value&page=2&per_page=5"));
        }

        [Test]
        public void CreateQuery_CalledWithNothing_CreatesNothing()
        {
            // Given
            var factory = new QueryFactory();

            // When
            var result = factory.BuildQuery();

            // Then
            Assert.That(result, Is.EqualTo(""));
        }
    }
}
