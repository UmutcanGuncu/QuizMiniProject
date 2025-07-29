namespace Quiz.Application.DTOs.AuthenticationDtos.Response;

public class LoginUserResponseDto
{
    public bool IsSuccess { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    //Jwt token bilgisini de geri döndürecek token sınıfını eklem
}