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

    private static void EnsureSuccessStatusCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = response.Content.ReadAsStringAsync().Result;
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}. Error: {errorMessage}");
        }
    }
}