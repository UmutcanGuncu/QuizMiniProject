using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Questions;
using Quiz.Application.CQRS.Queries.Questions;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Persistence.Contexts;

namespace Quiz.API.Controllers;

[Route("api")]
[ApiController]
public class QuestionController(IMediator mediator) : ControllerBase
{
    [HttpGet("questions")]
    [AllowAnonymous]
    public async Task<IActionResult> QuestionList()
    {
        var questions = await mediator.Send(new GetQuestionsQuery());
        if(questions is null)
            return NotFound();
        return Ok(questions);
    }

    [HttpGet("question/random")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> RandomQuestion()
    {
        var result = await mediator.Send(new GetRandomQuestionQuery());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("answer")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> AnswerQuestion(AnswerQuestionCommandRequest answerQuestionCommandRequest)
    {
        var result = await mediator.Send(answerQuestionCommandRequest);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

}