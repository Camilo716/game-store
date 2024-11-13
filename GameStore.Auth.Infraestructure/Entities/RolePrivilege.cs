using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Infraestructure.Entities;

public class RolePrivilege
{
    public string RoleId { get; set; }

    public Guid PrivilegeId { get; set; }

    public IdentityRole Role { get; set; }

    public Privilege Privilege { get; set; }
}