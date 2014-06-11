using System.Text;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Contracts.Tests.Queries
{
    public class TypeQueryFixture
    {
        [Test]
        public void AppendQuery_TypeValuesSet_AddsQuery()
        {
            // Given
            var query = new TypeQuery {Type = ZendeskType.Ticket};

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo("type:ticket"));
        }

        [Test]
        public void AppendQuery_AllValuesSet_AddsQuery()
        {
            // Given
            var query = new TypeQuery { Type = ZendeskType.Ticket, CustomField = "field", CustomFieldValue = "some value"};

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo("type:ticket field:some value"));
        }

        [Test]
        public void AppendQuery_CustomValuesSetWithoutType_AppendsNothing()
        {
            // Given
            var query = new TypeQuery { CustomField = "field", CustomFieldValue = "some value" };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }


        [Test]
        public void AppendQuery_NothingSet_AppendsNothing()
        {
            // Given
            var query = new TypeQuery { };

            // When
            var result = query.AppendQuery(new StringBuilder());

            // Then
            Assert.That(result.ToString(), Is.EqualTo(""));
        }
    }
}
