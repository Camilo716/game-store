using GameStore.Core.Auth;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Infraestructure.Auth;

public class BannedUserHandler(ITokenValidator tokenValidator) : AuthorizationHandler<BannedUserRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, BannedUserRequirement requirement)
    {
        if (IsAuthenticated(context) && tokenValidator.IsUnbanned(context.User))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private static bool IsAuthenticated(AuthorizationHandlerContext context)
    {
        return context.User.Identity?.IsAuthenticated == true;
    }
}