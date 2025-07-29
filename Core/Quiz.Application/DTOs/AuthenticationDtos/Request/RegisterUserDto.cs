namespace Quiz.Application.DTOs.AuthenticationDtos.Request;

public class RegisterUserDto
{
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}