using Quiz.Application.DTOs;

namespace Quiz.Application.CQRS.Results.Auths;

public class GoogleLoginCommandResult
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public Token Token { get; set; }
}