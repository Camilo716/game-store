using System.Security.Claims;
using GameStore.Auth.Core.Enums;
using GameStore.Auth.Core.Interfaces;

namespace GameStore.Auth.Infraestructure.Token;

public class TokenValidator : ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims
            .Any(c => c.Type == nameof(ClaimType.Permission) && c.Value == permission);
    }
}