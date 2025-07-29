using MediatR;
using Quiz.Application.CQRS.Results.Auths;

namespace Quiz.Application.CQRS.Commands.Auths;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResult>
{
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}