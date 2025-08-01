namespace Quiz.Application.CQRS.Results.Questions;

public class AnswerQuestionCommandResponse
{
    public bool Corrected { get; set; }
    public int? TotalPoint { get; set; }
}