namespace GameStore.Auth.Core.Models;

public class RoleModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public int? NormalizedName { get; set; }

    public List<PrivilegeModel> Privileges { get; set; } =
    [
    ];
}