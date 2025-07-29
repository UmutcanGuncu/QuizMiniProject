
using AutoMapper;
using Quiz.Application.CQRS.Commands.Auths;
using Quiz.Application.CQRS.Commands.Questions;
using Quiz.Application.CQRS.Results.Auths;
using Quiz.Application.CQRS.Results.Questions;
using Quiz.Application.DTOs.AuthenticationDtos.Request;
using Quiz.Application.DTOs.AuthenticationDtos.Response;
using Quiz.Application.DTOs.QuestionDtos.RequestDtos;
using Quiz.Application.DTOs.QuestionDtos.ResultDtos;
using Quiz.Domain.Entities;

namespace Quiz.Application.Profiles;

public class MappingProfile: Profile
{
   public MappingProfile()
   {
      CreateMap<AnswerQuestionCommandRequest, AnswerQuestionDto>();
      CreateMap<GetRandomQuestionResultDto, GetRandomQuestionQueryResult>();
      CreateMap<RegisterUserDto,AppUser>();
      CreateMap<RegisterUserCommandRequest, RegisterUserDto>();
      CreateMap<RegisterUserResponseDto, RegisterUserCommandResult>();
      CreateMap<LoginUserCommandRequest, LoginUserDto>();
      CreateMap<LoginUserResponseDto, LoginUserCommandResult>();
   }
}