namespace GameStore.Auth.Core.Models;

public class PrivilegeModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Key { get; set; }

    public List<RoleModel> Roles { get; set; } =
    [
    ];
}