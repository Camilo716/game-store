namespace GameStore.Auth.Core.Models;

public class RoleModel
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public List<PrivilegeModel> Privileges { get; set; } =
    [
    ];
}