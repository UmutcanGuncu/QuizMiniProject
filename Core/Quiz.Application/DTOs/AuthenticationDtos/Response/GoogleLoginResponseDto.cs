namespace Quiz.Application.DTOs.AuthenticationDtos.Response;

public class GoogleLoginResponseDto
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public Token Token { get; set; }
}