using MediatR;
using Quiz.Application.CQRS.Results.Questions;

namespace Quiz.Application.CQRS.Queries.Questions;

public class GetRandomQuestionQuery : IRequest<GetRandomQuestionQueryResult?>
{
    
}