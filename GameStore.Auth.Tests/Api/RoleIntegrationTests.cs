namespace GameStore.Auth.Tests.Api;

public class RoleIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllPrivileges()
    {
        var response = await HttpClient.GetAsync("api/roles/permissions");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}