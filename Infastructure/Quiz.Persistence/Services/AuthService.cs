using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Quiz.Application.Abstractions;
using Quiz.Application.DTOs.AuthenticationDtos.Request;
using Quiz.Application.DTOs.AuthenticationDtos.Response;
using Quiz.Domain.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Quiz.Persistence.Services;

public class AuthService(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,IMapper mapper, IRoleService roleService, ITokenService tokenService) : IAuthService
{
    // Kayıt olma ve giriş yapma işlemleri gerçekleştirildi
    public async Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto)
    {
        var user = mapper.Map<AppUser>(dto);
        user.UserName = dto.Email;
        var result = await userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
            AppUser appUser = await userManager.FindByNameAsync(dto.Email);
            await roleService.AssignUserToRoleAsync(user, "User");
            return new()
            {
                IsSuccess = true,
                Message = "Kullanıcı Kaydı Başarıyla Tamamlanmıştır"
            };
        }

        return new()
        {
            IsSuccess = false,
            Message = result.Errors.First().Description
        };
    }

    public async Task<LoginUserResponseDto> LoginUserAsync(LoginUserDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return new()
            {
                IsSuccess = false,
                Message = "Kullanıcı Bulunamadı"
            };
        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (signInResult.Succeeded)
        {
            await signInManager.PasswordSignInAsync(user, dto.Password, true, false);
            var token = await tokenService.CreateAccessTokenAsync(user.Id.ToString());
            return new()
            {
                IsSuccess = true,
                Message = "Giriş İşlemi Başarıyla Gerçekleştirilmiştir",
                Token = token,
                UserId = user.Id
            };
        }

        return new()
        {
            IsSuccess = false,
            Message = "E Posta Adresi veya Şifreniz Yanlış"
        };
    }

    public async Task<GoogleLoginResponseDto> GoogleLoginUserAsync(GoogleLoginDto dto)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<String> { "" } // google login işlemi için gerekli adres bilgisi ayarlandı
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken,settings);
        var info = new UserLoginInfo(dto.Provider, payload.Subject, dto.Provider);
        AppUser appUser = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        bool result = appUser != null;
        if (appUser == null)
        {
            appUser = await userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                appUser = new()
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    UserName = payload.Email,
                    NameSurname = payload.Name
                };
                var identityResult = await userManager.CreateAsync(appUser);
                result = identityResult.Succeeded;
            }
            else
            {
                await signInManager.SignInAsync(appUser, true);
                var token = await tokenService.CreateAccessTokenAsync(appUser.Id.ToString());
                return new()
                {
                    Succeeded = true,
                    Message = "Giriş İşlemi Başarıyla Gerçekleştirildi",
                    Token = token,
                    UserId = appUser.Id.ToString()
                };
            }
        }

        if (result)
        {
            await userManager.AddLoginAsync(appUser, info);
            await roleService.AssignUserToRoleAsync(appUser, "User");
            await signInManager.SignInAsync(appUser, true);
            var token = await tokenService.CreateAccessTokenAsync(appUser.Id.ToString());
            return new()
            {
                Succeeded = true,
                Message = "Giriş İşlemi Başarıyla Gerçekleştirildi",
                Token = token,
                UserId = appUser.Id.ToString()
            };
        }

        return new()
        {
            Succeeded = false,
            Message = "Giriş İşlemi Gerçekleştirilemedi"
        };
    }
}