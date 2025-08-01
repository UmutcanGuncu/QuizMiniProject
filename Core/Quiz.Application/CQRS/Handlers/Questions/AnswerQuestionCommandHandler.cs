using AutoMapper;
using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Commands.Questions;
using Quiz.Application.CQRS.Results.Questions;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;

namespace Quiz.Application.CQRS.Handlers.Questions;

public class AnswerQuestionCommandHandler(IQuestionService questionService, IMapper mapper) : IRequestHandler<AnswerQuestionCommandRequest, AnswerQuestionCommandResponse>
{
    public async Task<AnswerQuestionCommandResponse> Handle(AnswerQuestionCommandRequest request, CancellationToken cancellationToken)
    {
        var dto =  mapper.Map<AnswerQuestionDto>(request);
        var result = await questionService.AnswerQuesionAsync(dto);
        if(result == null)
            return null;
        return new()
        {
            Corrected = result.Corrected,
            TotalPoint = result.TotalPoint
        };
    }
}