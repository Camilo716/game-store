using System.Security.Claims;
using GameStore.Core.Auth;

namespace GameStore.Infraestructure.Auth;

public class TokenValidator : ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims
            .Any(c => c.Type == nameof(Policy.Permission) && c.Value == permission);
    }

    public bool IsUnbanned(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims
            .Any(c => c.Type == nameof(Policy.NotBanned) && c.Value == true.ToString());
    }
}