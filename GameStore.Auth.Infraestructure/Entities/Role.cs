using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Infraestructure.Entities;

public class Role : IdentityRole
{
    public string? ParentRoleId { get; set; }

    public List<Privilege> Privileges { get; set; } =
    [
    ];

    public List<Role> ChildrenRoles { get; set; } =
    [
    ];

    public Role? ParentRole { get; set; }
}