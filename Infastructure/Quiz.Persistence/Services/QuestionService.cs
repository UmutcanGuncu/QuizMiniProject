using Microsoft.EntityFrameworkCore;
using Quiz.Application.Abstractions;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Application.DTOs.QuestionDtos.ResultDtos;
using Quiz.Persistence.Contexts;

namespace Quiz.Persistence.Services;

public class QuestionService(QuizDbContext context, IUserService userService) : IQuestionService
{
    public async Task<IEnumerable<GetQuestionResultDto>?> GetAllQuestionsAsync()
    {
        var questions = await context.Questions.OrderBy(x=>x.CreatedAt).ToListAsync();
        if(questions.Count == 0)
            return null;
        return questions.Select(x => new GetQuestionResultDto()
        {
            QuestionId = x.Id,
            QuestionText = x.QuestionText,
            Options = x.Options,
            CorrectAnswer = x.CorrectAnswer
        });
    }

    public async Task<GetRandomQuestionResultDto?> GetRandomQuestionAsync()
    {
        var questions = await context.Questions.OrderBy(x=>x.CreatedAt).ToListAsync();
        if (questions.Count == 0)
            return null;
        Random random = new Random();
        int number = random.Next(0, questions.Count);
        
        var result = questions[number];
        return new GetRandomQuestionResultDto()
        {
            QuestionId = result.Id,
            QuestionText = result.QuestionText,
            Options = result.Options,
            CorrectAnswer = result.CorrectAnswer
        };
    }

    public async Task<AnswerQuestionResultDto?> AnswerQuesionAsync(AnswerQuestionDto answerQuestionDto)
    {
        // Gönderilen soru id'si ile eşleşen soru olup olmadığı kontrol edilmektedir.
        var question = await context.Questions.FindAsync(answerQuestionDto.QuestionId);
        // Eşleşen soru bulunmaması durumunda null döndürülmektedir.
        if (question == null)
            return null;
        int? totalUserPoint;
        // Soru bulunması durumunda sorunun cevabı ile kullanıcının cevabı karşılaştırılmaktadır
        if (question.CorrectAnswer == answerQuestionDto.SelectedOption) 
        {
            // Kullanıcı 60 saniyenin altında cevaplarsa 10 puan almakta eğer daha uzun sürede cevaplarsa 0 puan almaktadır
            bool inTime = (DateTime.UtcNow - answerQuestionDto.Created).TotalSeconds <= 60;
            if (inTime)
                  totalUserPoint = await userService.UpdateUserPointAsync(10);
            else
            {
                totalUserPoint = await userService.UpdateUserPointAsync(0);
            }
           
            return new AnswerQuestionResultDto()
            {
                Corrected = true,
                TotalPoint = totalUserPoint
            };
        }
        totalUserPoint = await userService.UpdateUserPointAsync(0);
           // Cevabın yanlış olması durumunda geri döndürülecek sınıf
        return new AnswerQuestionResultDto()
        {
            Corrected = false,
            TotalPoint = totalUserPoint
        };
    }
}