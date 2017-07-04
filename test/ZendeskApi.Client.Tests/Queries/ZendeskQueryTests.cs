using System;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.Queries
{
    public class ZendeskQueryTests
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
            var query = new ZendeskQuery().WithTypeFilter<TicketResponse>();
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
            var query = new ZendeskQuery().WithTypeFilter<TicketResponse>();
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
            var query = new ZendeskQuery().WithTypeFilter<TicketResponse>();
            query.WithFilter("name", "cheese factory", FilterOperator.Excludes).WithOrdering(SortBy.Priority, SortOrder.Asc);

            // When 
            var queryString = query.BuildQuery();

            // Then
            Assert.Equal("query=type:ticket+-name:cheese+factory&sort_by=priority&sort_order=asc", queryString);
        }


        // Examples from site
        [Fact]
        public void SmokeTestQueries()
        {
            Assert.Equal("query=123", new ZendeskQuery().WithTicketId(123).BuildQuery());
            Assert.Equal("query=type:user", new ZendeskQuery().WithTypeFilter<UserResponse>().BuildQuery());
            Assert.Equal("query=124+type:user", new ZendeskQuery().WithTicketId(124).WithTypeFilter<UserResponse>().BuildQuery());
            Assert.Equal("query=124+type:user", new ZendeskQuery().WithTypeFilter<UserResponse>().WithTicketId(124).BuildQuery());
            Assert.Equal("query=Kung+type:user", new ZendeskQuery().WithWord("Kung").WithTypeFilter<UserResponse>().BuildQuery());
            Assert.Equal("query=type:user+\"Jane+Doe\"", new ZendeskQuery().WithTypeFilter<UserResponse>().WithWord("Jane Doe", FilterOperator.Exact).BuildQuery());
            Assert.Equal("query=type:user+status:open", new ZendeskQuery().WithTypeFilter<UserResponse>().WithTicketStatus(TicketStatus.Open).BuildQuery());
            Assert.Equal("query=type:organization+created<2015-05-01", new ZendeskQuery().WithTypeFilter<Organization>().WithCreatedDate(new DateTime(2015,5,1), FilterOperator.LessThan).BuildQuery());
            Assert.Equal("query=type:organization+created>2015-05-01", new ZendeskQuery().WithTypeFilter<Organization>().WithCreatedDate(new DateTime(2015, 5, 1), FilterOperator.GreaterThan).BuildQuery());
            Assert.Equal("query=status<solved+requester:user%40domain.com+type:ticket", new ZendeskQuery().WithTicketStatus(TicketStatus.Solved, FilterOperator.LessThan).FromRequester("user@domain.com").WithTypeFilter<TicketResponse>().BuildQuery());
            Assert.Equal("query=type:user+tags:premium_support", new ZendeskQuery().WithTypeFilter<UserResponse>().WithTags("premium_support").BuildQuery());
            Assert.Equal("query=created>2012-07-17+type:ticket+organization:\"MD+Photo\"", new ZendeskQuery().WithCreatedDate(new DateTime(2012, 7, 17), FilterOperator.GreaterThan).WithTypeFilter<TicketResponse>().WithWordFromTypeFilter<Organization>("MD Photo",  FilterOperator.Exact).BuildQuery());
        }
    }
}
