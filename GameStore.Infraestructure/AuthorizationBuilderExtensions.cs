using GameStore.Core.Auth;
using GameStore.Infraestructure.Auth;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Infraestructure;

public static class AuthorizationBuilderExtensions
{
    public static AuthorizationBuilder AddPermissionPolicy(this AuthorizationBuilder builder, Permissions permission)
    {
        return builder.AddPolicy($"{permission}", policy =>
            policy.Requirements.Add(new PermissionRequirement($"{permission}")));
    }
}