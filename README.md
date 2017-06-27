# Zendesk Api Client

A .net Zendesk Api Client NuGet package for use with the ZendeskApi v3

# Breaking Changes

## 3.x.x
 This is a complete rewrite. If you are using 2.x, then expect there to be breaking changes (for the better)

## Creating a client:
To register this in your DI, you just need to call...
```c#
services.AddZendeskClient("enpointurl", "username", "token");
```
then you can inject in IZendeskClient anywhere you want to use it. Here you can access all the resources available.

If however you want to use the client in a DI less environment you can do this

```c#
var zendeskOptions = new ZendeskOptions
{
   EndpointUri = "endpoint",
   Username = "username"
   Token = "token"
};

var zendeskOptionsWrapper = new OptionsWrapper<ZendeskOptions>(zendeskOptions);
var client = new ZendeskClient(new ZendeskApiClient(zendeskOptionsWrapper), _loggerFactory.CreateLogger<ZendeskClient>());
```

## Example methods:
```c#
var ticket = await client.Tickets.GetAsync(1234L); // Get ticket by Id
var tickets = await client.Tickets.GetAllAsync(new [] { 1234L, 4321L }); // 
var ticket = await client.Tickets.PutAsync(ticket);
var ticket = await client.Tickets.PostAsync(ticket);
await client.Tickets.DeleteAsync(1234L);
```
## Searching, paging and filtering:

### Query Options:
```csharp
IZendeskQuery query = new ZendeskQuery<Organization>();
query.WithPaging(pageNumber:2, pageSize:10);
query.WithCustomFilter(field:"name", value:"Coffee Express");
query.WithOrdering(orderBy:OrderBy.created_at, order:Order.Relevance);
IResponse<T> response = client.Search.Find<T>(query);
```
### Use:
```csharp
IListResponse<User> response = client.Search.Find<User>(
    new ZendeskQuery<User>()
    .WithCustomFilter("email", "jazzy.b@just-eat.com")
);
IListResponse<User> response = client.Search.Find(
    new ZendeskQuery<Organization>()
    .WithCustomFilter("name", "Cupcake Cafe")
);
```
## The Zendesk API

The zendesk api documentation is available at http://developer.zendesk.com/documentation/rest_api/introduction.html
Querying and searching is limited by the searchable fields on the zendesk api

