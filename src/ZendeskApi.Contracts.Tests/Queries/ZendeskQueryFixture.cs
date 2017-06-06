using Xunit;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;

namespace ZendeskApi.Contracts.Tests.Queries
{
    public class ZendeskQueryFixture
    {
        [Fact]
        public void CalledWithCustomFieldsEquals_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Organization>();
            query.WithCustomFilter("name", "cheese factory", FilterOperator.Equals);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+name:cheese+factory&sort_by=created_at&sort_order=desc&page=1&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsLessThan_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Organization>();
            query.WithCustomFilter("updated_at", "10/15/14", FilterOperator.GreaterThan);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+updated_at>10%2F15%2F14&sort_by=created_at&sort_order=desc&page=1&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsGreaterThan_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Organization>();
            query.WithCustomFilter("updated_at", "10/15/14", FilterOperator.LessThan);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+updated_at<10%2F15%2F14&sort_by=created_at&sort_order=desc&page=1&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsAndPage_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Organization>();
            query.WithCustomFilter("name", "cheese factory", FilterOperator.Equals).WithPaging(3, 15);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+name:cheese+factory&sort_by=created_at&sort_order=desc&page=3&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithDifferentType_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Ticket>();
            query.WithCustomFilter("name", "cheese factory", FilterOperator.Equals).WithPaging(3, 15);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+name:cheese+factory&sort_by=created_at&sort_order=desc&page=3&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithOrderSet_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Ticket>();
            query.WithCustomFilter("name", "cheese factory", FilterOperator.Equals).WithPaging(3, 15).WithOrdering(OrderBy.priority, Order.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+name:cheese+factory&sort_by=priority&sort_order=asc&page=3&per_page=15", queryString);
        }

        [Fact]
        public void CalledWithNotEqual_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery<Ticket>();
            query.WithCustomFilter("name", "cheese factory", FilterOperator.NotEqual).WithPaging(3, 15).WithOrdering(OrderBy.priority, Order.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+-name:cheese+factory&sort_by=priority&sort_order=asc&page=3&per_page=15", queryString);
        }
    }
}
