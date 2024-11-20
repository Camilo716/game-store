using System.Security.Claims;
using System.Text.Encodings.Web;
using GameStore.Core.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameStore.Tests.Api;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthScheme = "TestScheme";

    public static readonly Guid UserId = Guid.NewGuid();

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
        AddPermissionClaim(claims, Permissions.ViewGenres);
        AddPermissionClaim(claims, Permissions.AddGenre);
        AddPermissionClaim(claims, Permissions.UpdateGenre);
        AddPermissionClaim(claims, Permissions.DeleteGenre);

        AddPermissionClaim(claims, Permissions.ViewPlatforms);
        AddPermissionClaim(claims, Permissions.AddPlatform);
        AddPermissionClaim(claims, Permissions.UpdatePlatform);
        AddPermissionClaim(claims, Permissions.DeletePlatform);

        AddPermissionClaim(claims, Permissions.ViewPublishers);
        AddPermissionClaim(claims, Permissions.AddPublisher);
        AddPermissionClaim(claims, Permissions.UpdatePublisher);
        AddPermissionClaim(claims, Permissions.DeletePublisher);
    }

    private static void AddPermissionClaim(List<Claim> claim, Permissions permission)
    {
        claim.Add(new($"{ClaimType.Permission}", $"{permission}"));
    }
}
