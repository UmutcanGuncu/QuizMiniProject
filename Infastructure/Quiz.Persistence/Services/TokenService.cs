using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quiz.Application.Abstractions;
using Quiz.Application.DTOs;
using Quiz.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Quiz.Persistence.Services;

// jwt token oluşturma işlemleri gerçekleştirildi 
public class TokenService(IConfiguration configuration, IRoleService roleService, IUserService userService) : ITokenService
{
    public async Task<Token> CreateAccessTokenAsync(string userId)
    {
        Token token = new();
        SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        token.Expiration = DateTime.UtcNow.AddDays(3);
        var roleClaims = await roleService.GetUserClaimsAsync(userId);
        AppUser? user = await userService.FindUserAsync(userId);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            user != null ? new Claim(ClaimTypes.Name, user.Email) : null
        }.Union(roleClaims);
        JwtSecurityToken securityToken = new(
            audience: configuration["JWT:Audience"],
            issuer: configuration["JWT:Issuer"],
            signingCredentials: signingCredentials,
            claims: claims,
            expires: token.Expiration,
            notBefore: DateTime.UtcNow
        );
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}