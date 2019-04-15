using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class UsersResourceSampleSite : SampleSite<UserResponse>
    {
        public UsersResourceSampleSite(string resource)
            : base(resource, MatchesRequest, ConfigureWebHost, PopulateState)
        { }

        private static void ConfigureWebHost(WebHostBuilder builder)
        {
            builder
                .ConfigureServices(services => {
                    services.AddSingleton(_ => new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<UserUpdateRequest, UserResponse>();
                        cfg.CreateMap<UserCreateRequest, UserResponse>();
                    }).CreateMapper());
                });
        }

        private static void PopulateState(State<UserResponse> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserResponse
                {
                    Id = i,
                    Name = $"name.{i}",
                    Email = $"email.{i}",
                    ExternalId = i.ToString(),
                    DefaultGroupId = i,
                    OrganizationId = i
                });
            }
        }

        private static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/show_many", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        IList<UserResponse> users = new List<UserResponse>();

                        var idsQuery = req.Query.ContainsKey("ids")
                            ? req.Query["ids"].ToString()
                            : req.Query["external_ids"].ToString();

                        var ids = idsQuery
                            .Split(',')
                            .Select(long.Parse)
                            .ToList();

                        if (ids.First() == long.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (req.Query.ContainsKey("ids"))
                        {
                            users = state.Items
                                .Select(x => x.Value)
                                .Where(x => ids.Contains(x.Id))
                                .ToList();
                        }
                        else if (req.Query.ContainsKey("external_ids"))
                        {
                            users = state.Items
                                .Select(x => x.Value)
                                .Where(x => ids.Contains(long.Parse(x.ExternalId)))
                                .ToList();
                        }

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            users = users
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users,
                            Count = users.Count
                        });
                    })
                    .MapGet("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var user = state.Items[id];

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = user
                        });
                    })
                    .MapGet("api/v2/users", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        var users = state.Items
                            .Select(x => x.Value)
                            .ToList();

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            if (page == int.MaxValue && size == int.MaxValue)
                            {
                                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                                return Task.FromResult(resp);
                            }

                            users = users
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int) HttpStatusCode.OK;

                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users,
                            Count = users.Count
                        });
                    })
                    .MapGet("api/v2/groups/{id}/users", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        if (id == int.MaxValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.FromResult(resp);
                        }

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        var users = state
                            .Items
                            .Where(x => x.Value.DefaultGroupId == id)
                            .Select(p => p.Value)
                            .ToList();

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            users = users
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int) HttpStatusCode.OK;

                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users,
                            Count = users.Count
                        });
                    })
                    .MapGet("api/v2/organizations/{id}/users", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        if (id == int.MaxValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.FromResult(resp);
                        }

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        var users = state
                            .Items
                            .Where(x => x.Value.OrganizationId.HasValue)
                            .Where(x => x.Value.OrganizationId == id)
                            .Select(p => p.Value)
                            .ToList();

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            users = users
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int) HttpStatusCode.OK;

                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users,
                            Count = users.Count
                        });
                    })
                    .MapGet("api/v2/incremental/users", (req, resp, routeData) =>
                    {
                        if (!req.Query.ContainsKey("start_time"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.BadRequest;

                            return Task.CompletedTask;
                        }

                        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var startTime = long.Parse(req.Query["start_time"]);
                        // Adding 1 second since the query param does not have millisecond accuracy
                        var startDateTime = epoch.AddSeconds(startTime + 1L);

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        var users = state
                            .Items
                            .Select(p => p.Value)
                            .Where(u => u.UpdatedAt > startDateTime)
                            .ToList();

                        var nextPage = users.Count > 0 ? Convert.ToInt64((users.Max(u => u.UpdatedAt) - epoch).TotalSeconds) : startTime;
                        var nextPageUrl = $"/api/v2/incremental/users?start_time={nextPage}";

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new IncrementalUsersResponse<UserResponse>(nextPage)
                        {
                            Users = users,
                            Count = users.Count,
                            NextPage = new Uri(new Uri("http://kung.fu"), nextPageUrl)
                        });
                    })
                    .MapPost("api/v2/users", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserRequest<UserCreateRequest>>().User;

                        if (string.IsNullOrEmpty(user.Name))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        if (user.Tags != null && user.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int) HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();

                        var userNew = mapper.Map<UserResponse>(user);

                        userNew.Id = long.Parse(Rand.Next().ToString());
                        userNew.Url = new Uri($"https://company.zendesk.com/api/v2/users/{userNew.Id}.json");
                        userNew.CreatedAt = DateTime.UtcNow;
                        userNew.UpdatedAt = DateTime.UtcNow;
                        state.Items.Add(userNew.Id, userNew);

                        resp.StatusCode = (int) HttpStatusCode.Created;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = userNew
                        });
                    })
                    .MapPost("api/v2/users/create_or_update", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserRequest<UserCreateRequest>>().User;
                        var id = user.ExternalId?.GetHashCode() ?? user.Email?.GetHashCode();
                        if (id == null)
                        {
                            resp.StatusCode = 422;
                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        var userNew = mapper.Map<UserResponse>(user);

                        userNew.Id = id.Value;
                        userNew.Url = new Uri($"https://company.zendesk.com/api/v2/users/{userNew.Id}.json");

                        if (state.Items.ContainsKey(userNew.Id))
                        {
                            var existingUser = state.Items[userNew.Id];
                            userNew = mapper.Map(userNew, existingUser);
                            userNew.UpdatedAt = DateTime.UtcNow;
                            resp.StatusCode = (int)HttpStatusCode.OK;
                        }
                        else
                        {
                            userNew.CreatedAt = DateTime.UtcNow;
                            userNew.UpdatedAt = DateTime.UtcNow;
                            state.Items.Add(userNew.Id, userNew);
                            resp.StatusCode = (int)HttpStatusCode.Created;
                        }
                        
                        
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = userNew
                        });
                    })
                    .MapPut("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserRequest<UserUpdateRequest>>().User;

                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        if (user.Tags != null && user.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int) HttpStatusCode.BadRequest;

                            return Task.CompletedTask;
                        }

                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        mapper.Map(user, state.Items[id]);

                        resp.StatusCode = (int) HttpStatusCode.Created;
                        return resp.WriteAsJson(new
                        {
                            user
                        });
                    })
                    .MapDelete("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        state.Items.Remove(id);

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return Task.CompletedTask;
                    });
            }
        }
    }
}
