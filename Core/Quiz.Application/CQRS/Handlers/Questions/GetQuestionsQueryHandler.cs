using MediatR;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Queries.Questions;
using Quiz.Application.CQRS.Results.Questions;

namespace Quiz.Application.CQRS.Handlers.Questions;

public class GetQuestionsQueryHandler(IQuestionService questionService) : IRequestHandler<GetQuestionsQuery, IEnumerable<GetQuestionsQueryResult>>
{
    public async Task<IEnumerable<GetQuestionsQueryResult>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        var results = await questionService.GetAllQuestionsAsync();
        return results.Select(x=> new GetQuestionsQueryResult()
        {
            QuestionId = x.QuestionId,
            QuestionText = x.QuestionText,
            Options = x.Options,
            CorrectAnswer = x.CorrectAnswer,
        });
    }
}