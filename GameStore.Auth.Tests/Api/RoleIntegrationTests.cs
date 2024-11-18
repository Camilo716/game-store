using System.Net;
using System.Net.Http.Json;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Api;

public class RoleIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetAllPermissions_WithoutPagination_ReturnsAllPermissions()
    {
        var response = await HttpClient.GetAsync("api/roles/permissions");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Post_GivenValidRole_CreatesRole()
    {
        CreateRoleRequest validRole = new()
        {
            Role = new RoleModel() { Id = Guid.NewGuid().ToString(), Name = "newRole" },
            Permissions =
            [
                PrivilegeSeed.AddGame.Id,
            ],
        };

        var response = await HttpClient.PostAsJsonAsync("api/roles", validRole);

        response.EnsureSuccessStatusCode();
        Assert.Equal(RoleSeed.GetRoles().Count + 1, DbContext.Roles.Count());
    }

    [Fact]
    public async Task Put_GivenValidRole_UpdatesRole()
    {
        CreateRoleRequest validRole = new()
        {
            Role = new RoleModel() { Id = RoleSeed.Admin.Id, Name = "AdminUpdated" },
            Permissions =
            [
                PrivilegeSeed.AddGame.Id,
            ],
        };

        var response = await HttpClient.PutAsJsonAsync("api/roles", validRole);

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetPermissionsByRoleId_WithoutPagination_ReturnsSuccess()
    {
        var roleId = RoleSeed.Manager.Id;

        var response = await HttpClient.GetAsync($"api/roles/{roleId}/permissions");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllRoles()
    {
        var response = await HttpClient.GetAsync("api/roles");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesRole()
    {
        string id = RoleSeed.Admin.Id;

        var response = await HttpClient.DeleteAsync($"api/roles/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(RoleSeed.GetRoles().Count - 1, DbContext.Roles.Count());
    }
}