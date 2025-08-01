namespace Quiz.Application.DTOs.AuthenticationDtos.Request;

public class GoogleLoginDto
{
   
    public string IdToken { get; set; }
    public string Provider { get; set; }
}