using MediatR;
using Quiz.Application.CQRS.Results.Auths;

namespace Quiz.Application.CQRS.Commands.Auths;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}