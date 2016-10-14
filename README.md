# Zendesk Api Client #

A .net Zendesk Api Client NuGet package for use with the ZendeskApi v2

#Breaking Changes#

## 2.x.x
 - .Net Framework version 4.0 is no longer supported
 - In October 2016 there has been a change to the [IRestClient](https://github.com/justeat/ZendeskApiClient/blob/master/src/ZendeskApi.Client/Http/IRestClient.cs) interface, adding parameters for request logging. This change does not affect most users of this library; it is a drop-in replacement for consumers. Only if you implement your own version of `IRestClient` beware that there will be a small change. If you do not perform per-call logging of ReST-y calls, feel free to ignore the parameters in your implementation.

##Creating a client:##

    IZendeskClient client = new ZendeskClient(
        new Uri("https://my-zendesk-api-host-endpoint"),
        new ZendeskDefaultConfiguration ("my-zendesk-username", 
        "my-zendesk-token")
    );

##Accessing resources:##

    client.Tickets...
    client.Organizations...
    client.Users...
    client.Groups...
    
##Http Methods:##

    IResponse<Ticket> response = client.Tickets.Get((long)1234);
    IListResponse<Ticket> response = client.Tickets.GetAll(new List<long> { 1234, 4321 });
    IResponse<Ticket> response = client.Tickets.Put(new TicketRequest { Item = ticket });
    IResponse<Ticket> response = client.Tickets.Post(new TicketRequest { Item = ticket });
    client.Tickets.Delete((long)1234));
    
##Searching, paging and filtering:##

###Query Options:###

    IZendeskQuery query = new ZendeskQuery<Organization>();
    query.WithPaging(pageNumber:2, pageSize:10);
    query.WithCustomFilter(field:"name", value:"Coffee Express");
    query.WithOrdering(orderBy:OrderBy.created_at, order:Order.Relevance);
    IResponse<T> response = client.Search.Find<T>(query);

###Use:###

    IListResponse<User> response = client.Search.Find<User>(
        new ZendeskQuery<User>()
        .WithCustomFilter("email", "jazzy.b@just-eat.com")
    );
    IListResponse<User> response = client.Search.Find(
        new ZendeskQuery<Organization>()
        .WithCustomFilter("name", "Cupcake Cafe")
    );
    
##The Zendesk API:##

The zendesk api documentation is available at http://developer.zendesk.com/documentation/rest_api/introduction.html
Querying and searching is limited by the searchable fields on the zendesk api

