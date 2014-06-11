using System.Text;
using JustEat.ZendeskApi.Contracts.Queries;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Contracts.Tests.Queries
{
    public class PagingQueryFixture
    {
        [Test]
        public void AppendQuery_ValuesSet_AddsQuery()
        {
            // Given
            var query = new PagingQuery{ PageNumber = 1, PageSize = 2 };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo("page=1&per_page=2"));
        }

        [Test]
        public void AppendQuery_PageNotSet_AddsNothing()
        {
            // Given
            var query = new PagingQuery {  PageSize = 2 };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }

        [Test]
        public void AppendQuery_PageSizeNotSet_AddsNothing()
        {
            // Given
            var query = new PagingQuery { PageNumber = 2 };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }

        [Test]
        public void AppendQuery_NothingSet_AddsNothing()
        {
            // Given
            var query = new PagingQuery { };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }
    }
}
