using System.Security.Claims;
using System.Text.Encodings.Web;
using GameStore.Auth.Core.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameStore.Auth.Tests.Api;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthScheme = "TestScheme";

    public static readonly Guid UserId = Guid.Parse("687d7c63-9a15-4faf-af5a-140782baa24d");

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, UserId.ToString()),
        ];

        AddAllPermissions(claims);

        var identity = new ClaimsIdentity(claims, AuthScheme);
        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(
            principal,
            new AuthenticationProperties(),
            AuthScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private static void AddAllPermissions(List<Claim> claims)
    {
        AddPermissionClaim(claims, Permissions.ViewRoles);
        AddPermissionClaim(claims, Permissions.AddRole);
        AddPermissionClaim(claims, Permissions.UpdateRole);
        AddPermissionClaim(claims, Permissions.DeleteRole);

        AddPermissionClaim(claims, Permissions.ViewUsers);
        AddPermissionClaim(claims, Permissions.AddUser);
        AddPermissionClaim(claims, Permissions.UpdateUser);
        AddPermissionClaim(claims, Permissions.DeleteUser);
        AddPermissionClaim(claims, Permissions.BanUser);
    }

    private static void AddPermissionClaim(List<Claim> claim, Permissions permission)
    {
        claim.Add(new($"{Policy.Permission}", $"{permission}"));
    }
}
