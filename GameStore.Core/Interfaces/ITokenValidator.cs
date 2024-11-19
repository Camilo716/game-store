using System.Security.Claims;

namespace GameStore.Core.Interfaces;

public interface ITokenValidator
{
    public bool HasPermission(string permission, ClaimsPrincipal claimsPrincipal);
}