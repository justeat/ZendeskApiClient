using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;

namespace ZendeskApi.Client.Tests.Queries
{
    public class ZendeskQueryFixture
    {
        [Fact]
        public void CalledWithCustomFieldsEquals_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Organization>();
            query.WithFilter("name", "cheese factory", FilterOperator.Equals);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+name:cheese+factory", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsLessThan_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Organization>();
            query.WithFilter("updated_at", "10/15/14", FilterOperator.GreaterThan);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+updated_at>10%2F15%2F14", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsGreaterThan_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Organization>();
            query.WithFilter("updated_at", "10/15/14", FilterOperator.LessThan);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+updated_at<10%2F15%2F14", queryString);
        }

        [Fact]
        public void CalledWithCustomFieldsAndPage_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Organization>();
            query.WithFilter("name", "cheese factory", FilterOperator.Equals);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:organization+name:cheese+factory", queryString);
        }

        [Fact]
        public void CalledWithDifferentType_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Ticket>();
            query.WithFilter("name", "cheese factory", FilterOperator.Equals);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+name:cheese+factory", queryString);
        }

        [Fact]
        public void CalledWithOrderSet_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Ticket>();
            query.WithFilter("name", "cheese factory", FilterOperator.Equals).WithOrdering(SortBy.Priority, SortOrder.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+name:cheese+factory&sort_by=priority&sort_order=asc", queryString);
        }

        [Fact]
        public void CalledWithNotEqual_BuildsQuery()
        {
            // Given
            var query = new ZendeskQuery().WithTypeFilter<Ticket>();
            query.WithFilter("name", "cheese factory", FilterOperator.Excludes).WithOrdering(SortBy.Priority, SortOrder.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+-name:cheese+factory&sort_by=priority&sort_order=asc", queryString);
        }
    }
}
