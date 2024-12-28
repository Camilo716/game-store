using GameStore.Auth.Core.Role;

namespace GameStore.Auth.Core.Privilege;

public class PrivilegeModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Key { get; set; }

    public List<RoleModel> Roles { get; set; } =
    [
    ];
}