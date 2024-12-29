using System.Security.Claims;
using GameStore.Auth.Core.Config;
using GameStore.Auth.Core.Token;

namespace GameStore.Auth.Infraestructure.Token;

public class TokenValidator : ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims
            .Any(c => c.Type == nameof(Policy.Permission) && c.Value == permission);
    }
}