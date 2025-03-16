using GameStore.Auth.Core.Token;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Auth.Infraestructure.Handlers;

public class PermissionHandler(ITokenValidator tokenValidator) : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (IsAuthenticated(context) && HasRequiredPermission(context, requirement))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private bool HasRequiredPermission(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        return tokenValidator.HasPermission(requirement.Permission, context.User);
    }

    private static bool IsAuthenticated(AuthorizationHandlerContext context)
    {
        return context.User.Identity?.IsAuthenticated == true;
    }
}