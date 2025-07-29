using MediatR;
using Quiz.Application.CQRS.Results.Questions;

namespace Quiz.Application.CQRS.Commands.Questions;

public class AnswerQuestionCommandRequest : IRequest<AnswerQuestionCommandResponse>
{
    public Guid QuestionId { get; set; }
    public int SelectedOption { get; set; }
}