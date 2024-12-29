using System.Security.Claims;

namespace GameStore.Core.Auth;

public interface ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal);

    public bool IsUnbanned(ClaimsPrincipal claimsPrincipal);
}