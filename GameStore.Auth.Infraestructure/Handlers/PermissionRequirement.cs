using Microsoft.AspNetCore.Authorization;

namespace GameStore.Auth.Infraestructure.Handlers;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission => permission;
}