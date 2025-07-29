using Quiz.Domain.Entities.Common;

namespace Quiz.Domain.Entities;

public class Question : BaseEntity
{
    public string QuestionText { get; set; }
    public ICollection<string> Options { get; set; }
    public int CorrectAnswer { get; set; } //cevabın options listesindeki indis numarası tutulacaktır
}