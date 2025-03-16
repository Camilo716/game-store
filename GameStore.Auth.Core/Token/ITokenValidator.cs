using System.Security.Claims;

namespace GameStore.Auth.Core.Token;

public interface ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal);
}