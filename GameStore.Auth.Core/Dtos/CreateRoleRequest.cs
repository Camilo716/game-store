using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Dtos;

public class CreateRoleRequest
{
    public RoleModel Role { get; set; }

    public IEnumerable<Guid> Permissions { get; set; }
}