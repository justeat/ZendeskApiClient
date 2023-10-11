// See https://aka.ms/new-console-template for more information
using ZendeskApi.Client;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;

// The purpose of this code is to ilustrate how cursor based pagination can be used.
var services = new ServiceCollection();
services.AddZendeskClientWithHttpClientFactory("https://z3n-api-client-rb.zendesk.com", "api-client-rb+agent@zendesk.com", "tLzGI6Mfi4TwABtJKpRHGs4bE23JOecqUxTyE9Wx");
var serviceProvider = services.BuildServiceProvider();
var client = (ZendeskClient)serviceProvider.GetRequiredService<IZendeskClient>();

var cursorPager = new CursorPager { Size = 5 }; // forcing the pagination, default is 100. 

var ticketCursorResponse = await client.Tickets.GetAllAsync(cursorPager);

while (ticketCursorResponse.Meta.HasMore)
{
    Console.WriteLine("Number of Tickets on this page: " + ticketCursorResponse.Count());
    foreach(var ticket in ticketCursorResponse)
    {
        Console.WriteLine("the id of this ticket is:" + ticket.Id);
    }

    cursorPager.AfterCursor = ticketCursorResponse.Meta.AfterCursor; // make sure the next request includes the after_cursor
    Console.WriteLine("fetching the next page");
    ticketCursorResponse = await client.Tickets.GetAllAsync(cursorPager);        
}
