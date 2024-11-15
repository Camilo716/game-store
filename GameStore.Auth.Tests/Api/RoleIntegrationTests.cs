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
    public async Task GetPermissionsByRoleId_WithoutPagination_ReturnsSuccess()
    {
        var roleId = RoleSeed.Manager.Id;

        var response = await HttpClient.GetAsync($"api/roles/{roleId}/permissions");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}