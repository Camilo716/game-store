using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Enums;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Auth.Infraestructure.Token;

public class TokenGenerator(IConfiguration configuration) : ITokenGenerator
{
    private readonly string _secretKey = configuration["Jwt:SecretKey"]!;

    public AuthToken GenerateToken(UserModel userModel)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(userModel)),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthToken() { Token = tokenHandler.WriteToken(token) };
    }

    private static List<Claim> GetClaims(UserModel userModel)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, userModel.Id),
        ];

        // TODO: Read permissions from user role
        claims.Add(new(nameof(ClaimType.Permission), nameof(Permissions.ViewRoles)));
        return claims;
    }
}