using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZendeskApi.Client;
using ZendeskApi.Client.Responses;

public class CursorPaginatedIterator<T> : IEnumerable<T>
{

    public ICursorPagination<T> Response { get; set; }

    private IZendeskApiClient client;


    private string ResponseType { get; }

    public CursorPaginatedIterator(ICursorPagination<T> response, IZendeskApiClient client)
    {
        Response = response;
        this.client = client;
        ResponseType = response.GetType().FullName;
    }

    public bool HasMore() => Response.Meta.HasMore;

    public IEnumerator<T> GetEnumerator()
    {
        return Response.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Response.GetEnumerator();
    }


    public async Task NextPage()
    {
        await ExecuteRequest(Response.Links.Next);
    }

    public async Task PrevPage()
    {
        await ExecuteRequest(Response.Links.Prev);
    }

    public async IAsyncEnumerable<T> All()
    {
        foreach (var item in Response)
        {
            yield return item;
        }
        while (HasMore())
        {
            await NextPage();
            foreach (var item in Response)
            {
                yield return item;
            }
        }
        yield break;
    }

    private async Task ExecuteRequest(string requestUrl)
    {
        var httpResponseMessage = await client.CreateClient().GetAsync(requestUrl);
        var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
        Response = (ICursorPagination<T>)JsonConvert.DeserializeObject(responseBody, Type.GetType(ResponseType));
    }

}
