namespace Quiz.Application.DTOs.QuestionDtos.ResultDtos;

public class GetRandomQuestionResultDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public ICollection<string> Options { get; set; }
    public int CorrectAnswer { get; set; }
}