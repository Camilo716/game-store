using Microsoft.AspNetCore.Authorization;

namespace GameStore.Infraestructure.Auth;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission => permission;
}