namespace Quiz.Application.DTOs.QuestionDtos.RequestDtos;

public class AnswerQuestionDto
{
    public Guid QuestionId { get; set; }
    public int SelectedOption { get; set; }
    public DateTime Created { get; set; }
}