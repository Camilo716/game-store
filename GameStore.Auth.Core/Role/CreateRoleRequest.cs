namespace GameStore.Auth.Core.Role;

public class CreateRoleRequest
{
    public RoleModel Role { get; set; }

    public IEnumerable<Guid> Permissions { get; set; }
}