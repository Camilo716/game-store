using System.Net;
using System.Net.Http.Json;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Api;

public class UserIntegrationTest : BaseIntegrationTest
{
    [Fact]
    public async Task Post_GivenValidUser_CreatesUser()
    {
        CreateUserRequest validUser = new()
        {
            User = new UserModel() { Id = Guid.NewGuid().ToString(), UserName = "test" },
            Roles =
            [
                RoleSeed.Admin.Id,
            ],
            Password = "123aA!",
        };

        var response = await HttpClient.PostAsJsonAsync("api/users", validUser);

        EnsureSuccessStatusCode(response);
        Assert.Equal(UserSeed.GetUsers().Count + 1, DbContext.Users.Count());
    }

    [Fact]
    public async Task Put_GivenValidUser_UpdatesUser()
    {
        CreateUserRequest request = new()
        {
            User = new UserModel() { Id = UserSeed.UserManager.Id, UserName = "newName" },
            Roles =
            [
               RoleSeed.Manager.Id,
            ],
            Password = "newPassword123!",
        };

        var response = await HttpClient.PutAsJsonAsync("api/Users", request);

        Assert.NotNull(response);
        EnsureSuccessStatusCode(response);
    }

    [Fact]
    public async Task Login_GivenValidCredentials_ReturnsToken()
    {
        LoginRequest loginRequest = new()
        {
            Login = UserSeed.UserManager.UserName!,
            Password = UserSeed.TestPassword,
            InternalAuth = true,
        };

        var response = await HttpClient.PostAsJsonAsync("api/users/login", loginRequest);

        Assert.NotNull(response);
        EnsureSuccessStatusCode(response);
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/users");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetUserRoles_ReturnsSuccess()
    {
        string id = UserSeed.UserManager.Id;

        var response = await HttpClient.GetAsync($"api/users/{id}/roles");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesUser()
    {
        string id = UserSeed.UserManager.Id;

        var response = await HttpClient.DeleteAsync($"api/users/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(UserSeed.GetUsers().Count - 1, DbContext.Users.Count());
    }

    [Fact]
    public async Task GetUserBanDurations_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/users/ban/durations");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    private static void EnsureSuccessStatusCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = response.Content.ReadAsStringAsync().Result;
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}. Error: {errorMessage}");
        }
    }
}