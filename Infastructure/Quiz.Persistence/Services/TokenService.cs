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
        // Token sınıfından yeni nesne oluşturuldu
        Token token = new();
        //SymetricSecurityKey bilgisi verildi. Appsettings.Json dosyassında bulunan key bilgisi byte formatına dönüştürüldü
        SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        // Byte türüne dönüştürülmüş olan SymmetricSecurityKey'in şifreleme algoritması belirtildi
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        //Token'in geçerlilik süresi belirtildi
        token.Expiration = DateTime.UtcNow.AddDays(3);
        //Kullanıcının rol bilgileri getirildi
        var roleClaims = await roleService.GetUserClaimsAsync(userId);
        //kullanıcı bilgisi getirildi
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