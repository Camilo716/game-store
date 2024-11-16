using System.Security.Claims;

namespace GameStore.Auth.Core.Interfaces;

public interface ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal);
}