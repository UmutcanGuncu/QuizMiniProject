using AutoMapper;
using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Auths;
using Quiz.Application.CQRS.Results.Auths;
using Quiz.Application.DTOs.AuthenticationDtos.Request;

namespace Quiz.Application.CQRS.Handlers.Auths;

public class LoginUserCommandHandler(IAuthService authService, IMapper mapper) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResult>
{
    public async Task<LoginUserCommandResult> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var loginUserDto = mapper.Map<LoginUserDto>(request);
        var responseDto = await authService.LoginUserAsync(loginUserDto);
        return mapper.Map<LoginUserCommandResult>(responseDto);
    }
}