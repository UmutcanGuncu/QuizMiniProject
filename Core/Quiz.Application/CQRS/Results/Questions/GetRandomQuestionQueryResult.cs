namespace Quiz.Application.CQRS.Results.Questions;

public class GetRandomQuestionQueryResult 
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public ICollection<string> Options { get; set; }
    public int CorrectAnswer { get; set; } 
}