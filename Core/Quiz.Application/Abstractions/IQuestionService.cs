using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Application.DTOs.QuestionDtos.ResultDtos;

namespace Quiz.Application.Abstractions;

public interface IQuestionService
{
    Task<IEnumerable<GetQuestionResultDto>> GetAllQuestionsAsync();
    Task<GetRandomQuestionResultDto> GetRandomQuestionAsync();
    Task<AnswerQuestionResultDto> AnswerQuesionAsync(AnswerQuestionDto answerQuestionDto);
}