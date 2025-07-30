using Quiz.Application.DTOs.AuthenticationDtos.Request;
using Quiz.Application.DTOs.AuthenticationDtos.Response;

namespace Quiz.Application.Abstractions;

public interface IAuthService
{
    Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto);
    Task<LoginUserResponseDto> LoginUserAsync(LoginUserDto dto);
    Task<GoogleLoginResponseDto> GoogleLoginUserAsync(GoogleLoginDto dto);
}