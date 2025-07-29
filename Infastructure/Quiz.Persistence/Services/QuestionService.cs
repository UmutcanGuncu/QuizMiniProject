using Microsoft.EntityFrameworkCore;
using Quiz.Application.Abstractions;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Application.DTOs.QuestionDtos.ResultDtos;
using Quiz.Persistence.Contexts;

namespace Quiz.Persistence.Services;

public class QuestionService(QuizDbContext context) : IQuestionService
{
    public async Task<IEnumerable<GetQuestionResultDto>> GetAllQuestionsAsync()
    {
        var questions = await context.Questions.OrderBy(x=>x.CreatedAt).ToListAsync();
        return questions.Select(x => new GetQuestionResultDto()
        {
            QuestionId = x.Id,
            QuestionText = x.QuestionText,
            Options = x.Options,
            CorrectAnswer = x.CorrectAnswer
        });
    }

    public async Task<GetRandomQuestionResultDto> GetRandomQuestionAsync()
    {
        var questions = await context.Questions.OrderBy(x=>x.CreatedAt).ToListAsync();
        Random random = new Random();
        int number = random.Next(0, questions.Count+1);
        
        var result = questions[number];
        return new GetRandomQuestionResultDto()
        {
            QuestionId = result.Id,
            QuestionText = result.QuestionText,
            Options = result.Options,
            CorrectAnswer = result.CorrectAnswer
        };
    }

    public async Task<AnswerQuestionResultDto> AnswerQuesionAsync(AnswerQuestionDto answerQuestionDto)
    {
        var question = await context.Questions.FindAsync(answerQuestionDto.QuestionId);
        if (question == null)
            return null;
        if (question.CorrectAnswer == answerQuestionDto.SelectedOption)
            return new AnswerQuestionResultDto()
            {
                Corrected = true
            };
        return new AnswerQuestionResultDto()
        {
            Corrected = false
        };
    }
}