using MediatR;
using Quiz.Application.CQRS.Results.Auths;

namespace Quiz.Application.CQRS.Commands.Auths;

public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResult>
{
    public string IdToken { get; set; }
    public string Provider { get; set; }
}