# Zendesk Api Client
[![NuGet Version](https://img.shields.io/nuget/vpre/ZendeskApi.Client.svg?style=flat-square)](https://www.nuget.org/packages/ZendeskApi.Client)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ZendeskApi.Client.svg?style=flat-square)](https://www.nuget.org/packages/ZendeskApi.Client)
[![AppVeyor Build Status](https://img.shields.io/appveyor/ci/justeattech/zendeskapiclient/master.svg?style=flat-square)](https://ci.appveyor.com/project/justeattech/zendeskapiclient)
[![Gitter](https://img.shields.io/gitter/room/justeat/ZendeskApiClient.svg?style=flat-square)](https://gitter.im/justeat/ZendeskApiClient)

A .netstandard NuGet package for use with the  Zendesk v2 API.

# Breaking Changes

## 3.x.x
This is a complete rewrite so expect breaking changes.

Some noteworthy changes include:
- `PostAsync` replaced with `CreateAsync`
- `PutAsync` replaced with `UpdateAsync`
- `Search` resource now uses `SearchAsync` instead of `Find`, and introduces a new fluent api to replace the old `ZendeskQuery<T>` class.


## Creating a client
To register this in your DI, you just need to call...
```c#
services.AddZendeskClient("https://[your_url].zendesk.com", "username", "token");
```
then you can inject in IZendeskClient anywhere you want to use it. Here you can access all the resources available.

If however you want to use the client in a DI less environment you can do this

```c#
var zendeskOptions = new ZendeskOptions
{
   EndpointUri = "https://[your_url].zendesk.com",
   Username = "username"
   Token = "token"
};

var loggerFactory = new LoggerFactory();
var zendeskOptionsWrapper = new OptionsWrapper<ZendeskOptions>(zendeskOptions);
var client = new ZendeskClient(new ZendeskApiClient(zendeskOptionsWrapper), loggerFactory.CreateLogger<ZendeskClient>());
```

## Example methods
```c#
var ticket = await client.Tickets.GetAsync(1234L); // Get ticket by Id
var tickets = await client.Tickets.GetAllAsync(new [] { 1234L, 4321L }); // 
var ticket = await client.Tickets.UpdateAsync(ticket);
var ticket = await client.Tickets.CreateAsync(ticket);
await client.Tickets.DeleteAsync(1234L);
```

## Searching and filtering
```c#
await client.Search.SearchAsync<User>(q => 
    q.WithFilter("email", "jazzy.b@just-eat.com")
);

await client.Search.SearchAsync<Organization>(q => 
    q.WithFilter("name", "Cupcake Cafe")
);

// All non closed tickets
await client.Search.SearchAsync<Ticket>(q => 
    q.WithFilter("status", "closed", FilterOperator.LessThan)
);
```

## The Zendesk API

The zendesk api documentation is available at http://developer.zendesk.com/documentation/rest_api/introduction.html
Querying and searching is limited by the searchable fields on the zendesk api

## Contributing

In order to contribute to this repository, create a fork, branch from develop and then submit a pull request back to develop here. We will then build checks will then occur running unit tests. Once all is good we will merge. If you could write integration tests and run against your zendesk instance, that would be great! Once in develop, we can then create a Pull Request to master which will run the unit tests and integration tests and we can be super confident of pushing a package to nuget!

## Integration Tests

In order to run integration tests against your own zendesk instance use the Cake script provided by:

```powershell
.\build.ps1 -Target "Run-Integration-Tests" -ScriptArgs '-zendeskUrl="<your zendesk url>"', '-zendeskUsername="<your zendesk username>"', '-zendeskToken="<your zendesk token>"'
```