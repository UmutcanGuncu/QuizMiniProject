using AutoMapper;
using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Auths;
using Quiz.Application.CQRS.Results.Auths;
using Quiz.Application.DTOs.AuthenticationDtos.Request;

namespace Quiz.Application.CQRS.Handlers.Auths;

public class RegisterUserCommandHandler(IAuthService authService, IMapper mapper) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResult>
{
    public async Task<RegisterUserCommandResult> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var registerUserDto = mapper.Map<RegisterUserDto>(request);
        var responseDto = await authService.RegisterUserAsync(registerUserDto);
        return mapper.Map<RegisterUserCommandResult>(responseDto);
    }
}