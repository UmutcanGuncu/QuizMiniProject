namespace Quiz.Application.CQRS.Results.Auths;

public class RegisterUserCommandResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}