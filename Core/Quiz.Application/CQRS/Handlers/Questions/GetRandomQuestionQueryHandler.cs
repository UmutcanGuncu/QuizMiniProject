using AutoMapper;
using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Queries.Questions;
using Quiz.Application.CQRS.Results.Questions;
using Quiz.Application.DTOs.QuestionDtos.ResultDtos;

namespace Quiz.Application.CQRS.Handlers.Questions;

public class GetRandomQuestionQueryHandler(IQuestionService questionService, IMapper mapper) : IRequestHandler<GetRandomQuestionQuery, GetRandomQuestionQueryResult>
{
    public async Task<GetRandomQuestionQueryResult> Handle(GetRandomQuestionQuery request, CancellationToken cancellationToken)
    {
        var dto = await questionService.GetRandomQuestionAsync();
        var result = mapper.Map<GetRandomQuestionQueryResult>(dto);
        return result;
    }
}