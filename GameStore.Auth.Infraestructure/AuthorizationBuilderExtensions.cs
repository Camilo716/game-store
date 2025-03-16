using GameStore.Auth.Core.Config;
using GameStore.Auth.Infraestructure.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Auth.Infraestructure;

public static class AuthorizationBuilderExtensions
{
    public static AuthorizationBuilder AddPermissionPolicy(this AuthorizationBuilder builder, Permissions permission)
    {
        return builder.AddPolicy($"{permission}", policy =>
            policy.Requirements.Add(new PermissionRequirement($"{permission}")));
    }
}