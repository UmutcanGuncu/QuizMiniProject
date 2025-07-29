using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Quiz.Application.Abstractions;
using Quiz.Application.DTOs.AuthenticationDtos.Request;
using Quiz.Application.DTOs.AuthenticationDtos.Response;
using Quiz.Domain.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Quiz.Persistence.Services;

public class AuthService(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,IMapper mapper) : IAuthService
{
    
    public async Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto)
    {
        var user = mapper.Map<AppUser>(dto);
        user.UserName = dto.Email;
        var result = await userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
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
            return new()
            {
                IsSuccess = true,
                Message = "Giriş İşlemi Başarıyla Gerçekleştirilmiştir",
                UserId = user.Id
            };
        }

        return new()
        {
            IsSuccess = false,
            Message = "E Posta Adresi veya Şifreniz Yanlış"
        };
    }
}