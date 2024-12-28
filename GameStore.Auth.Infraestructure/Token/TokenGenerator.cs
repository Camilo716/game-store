using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStore.Auth.Core.Config;
using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Core.User;
using GameStore.Auth.Core.User.Login;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Auth.Infraestructure.Token;

public class TokenGenerator(
    IConfiguration configuration,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IUnitOfWork unitOfWork) : ITokenGenerator
{
    private string SecretKey => configuration["Jwt:SecretKey"]!;

    public async Task<AuthToken> GenerateTokenAsync(UserModel userModel)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        List<Claim> claims =
        [
           new(ClaimTypes.NameIdentifier, userModel.Id),
        ];

        await AddUserPermissionsClaimsAsync(userModel, claims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthToken() { Token = tokenHandler.WriteToken(token) };
    }

    private async Task AddUserPermissionsClaimsAsync(UserModel userModel, List<Claim> claims)
    {
        IEnumerable<string> permissions = await GetUserPermissionsAsync(userModel);

        foreach (string permission in permissions)
        {
            claims.Add(new(nameof(ClaimType.Permission), permission));
        }
    }

    private async Task<IEnumerable<string>> GetUserPermissionsAsync(UserModel userModel)
    {
        User user = await userManager.FindByIdAsync(userModel.Id)
            ?? throw new InvalidOperationException($"User {userModel.Id} not found.");

        var roles = await userManager.GetRolesAsync(user);

        if (!roles?.Any() ?? true)
        {
            return
            [
            ];
        }

        List<string> permissions =
        [
        ];

        foreach (string roleName in roles)
        {
            await AddRolePermissions(permissions, roleName);
        }

        return permissions.Distinct();
    }

    private async Task AddRolePermissions(List<string> permissions, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            return;
        }

        var rolePermissions = await unitOfWork.PrivilegeRepository.GetByRoleIdAsync(role.Id);
        permissions.AddRange(rolePermissions.Select(x => x.Key));
    }
}