using Quiz.Application.DTOs;

namespace Quiz.Application.CQRS.Results.Auths;

public class LoginUserCommandResult
{
    public bool IsSuccess { get; set; }
    public Guid? UserId { get; set; }
    public string Message { get; set; }
    public Token?  Token { get; set; } 
}