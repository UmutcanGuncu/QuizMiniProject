using AutoMapper;
using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Auths;
using Quiz.Application.CQRS.Results.Auths;
using Quiz.Application.DTOs.AuthenticationDtos.Request;

namespace Quiz.Application.CQRS.Handlers.Auths;

public class GoogleLoginCommandHandler(IAuthService authService, IMapper mapper) : IRequestHandler<GoogleLoginCommandRequest,GoogleLoginCommandResult> 
{
    public async Task<GoogleLoginCommandResult> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var googleLoginDto = mapper.Map<GoogleLoginDto>(request);
        var googleLoginResponse = await authService.GoogleLoginUserAsync(googleLoginDto);
        var result = mapper.Map<GoogleLoginCommandResult>(googleLoginResponse);
        return result;
    }
}