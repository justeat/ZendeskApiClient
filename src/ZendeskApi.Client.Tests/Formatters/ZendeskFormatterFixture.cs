using System.Collections.Generic;
using ZendeskApi.Client.Formatters;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Formatters
{
    public class ZendeskFormatterFixture
    {
        [Test]
        public void ToCsv_Called_ReturnsItemsAsZendeskCompliantCsv()
        {
            // Given
            var list = new List<string> {"a", "bb", "ccc"};

            // When
            var result = ZendeskFormatter.ToCsv(list);

            // Then
            Assert.That(result, Is.EqualTo("a,bb,ccc"));
        }

        [Test]
        public void ToCsv_Called_ReturnsItemsTrimmed()
        {
            // Given
            var list = new List<string> { "                  a", "bb               ", "c cc " };

            // When
            var result = ZendeskFormatter.ToCsv(list);

            // Then
            Assert.That(result, Is.EqualTo("a,bb,c cc"));
        }

        [Test]
        public void ToCsv_Called_ReturnsNumericItemsAsZendeskCompliantCsv()
        {
            // Given
            var list = new List<long> { 1, 22, 333 };

            // When
            var result = ZendeskFormatter.ToCsv(list);

            // Then
            Assert.That(result, Is.EqualTo("1,22,333"));
        }
    }
}
