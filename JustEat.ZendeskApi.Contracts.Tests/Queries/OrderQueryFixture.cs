using System.Text;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Contracts.Tests.Queries
{
    public class OrderQueryFixture
    {
        [Test]
        public void AppendQuery_TypeValuesSet_AddsQuery()
        {
            // Given
            var query = new OrderQuery { OrderBy = OrderBy.created_at };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo("sort_by=created_at&sort_order=asc"));
        }

        [Test]
        public void AppendQuery_TypeAndOrderValuesSet_AddsQuery()
        {
            // Given
            var query = new OrderQuery { OrderBy = OrderBy.created_at, Order = Order.Desc };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo("sort_by=created_at&sort_order=desc"));
        }

        [Test]
        public void AppendQuery_OrderValuesSet_ReturnsEmptyStringBuilder()
        {
            // Given
            var query = new OrderQuery { Order = Order.Desc };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }

        [Test]
        public void AppendQuery_NothingSet_ReturnsEmptyStringBuilder()
        {
            // Given
            var query = new OrderQuery { };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }
    }
}
