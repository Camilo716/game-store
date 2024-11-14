using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Auth.Infraestructure.Adapters;

public class TokenGeneratorAdapter(IConfiguration configuration) : ITokenGenerator
{
    private readonly string _secretKey = configuration["Jwt:SecretKey"]!;
    private readonly string _issuer = configuration["Jwt:Issuer"]!;
    private readonly string _audience = configuration["Jwt:Audience"]!;

    public AuthToken GenerateToken(UserModel userModel)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id),
            new Claim(JwtRegisteredClaimNames.Name, userModel.UserName),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new AuthToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }
}