using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Auths;
using Quiz.Application.DTOs.AuthenticationDtos.Request;

namespace Quiz.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommandRequest registerUserCommandRequest)
    {
        var result = await mediator.Send(registerUserCommandRequest);
        if(result.IsSuccess)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
    {
        var result = await mediator.Send(loginUserCommandRequest);
        if(result.IsSuccess)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
    {
        var result = await mediator.Send(googleLoginCommandRequest);
        if (result.Succeeded)
            return Ok(result);
        return NotFound(result);
    }
}