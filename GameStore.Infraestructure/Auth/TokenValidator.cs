using System.Security.Claims;
using GameStore.Core.Enums;
using GameStore.Core.Interfaces;

namespace GameStore.Infraestructure.Auth;

public class TokenValidator : ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims
            .Any(c => c.Type == nameof(ClaimType.Permission) && c.Value == permission);
    }
}