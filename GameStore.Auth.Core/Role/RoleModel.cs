using GameStore.Auth.Core.Privilege;

namespace GameStore.Auth.Core.Role;

public class RoleModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public List<PrivilegeModel> Privileges { get; set; } =
    [
    ];
}