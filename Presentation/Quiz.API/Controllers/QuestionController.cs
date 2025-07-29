using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Questions;
using Quiz.Application.CQRS.Queries.Questions;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Persistence.Contexts;

namespace Quiz.API.Controllers;

[Route("api")]
public class QuestionController(IMediator mediator) : ControllerBase
{
    [HttpGet("questions")]
    public async Task<IActionResult> QuestionList()
    {
        var questions = await mediator.Send(new GetQuestionsQuery());
        return Ok(questions);
    }

    [HttpGet("question/random")]
    public async Task<IActionResult> RandomQuestion()
    {
        var result = await mediator.Send(new GetRandomQuestionQuery());
        return Ok(result);
    }

    [HttpPost("answer")]
    public async Task<IActionResult> AnswerQuestion(AnswerQuestionCommandRequest answerQuestionCommandRequest)
    {
        var result = await mediator.Send(answerQuestionCommandRequest);
        return Ok(result);
    }

}