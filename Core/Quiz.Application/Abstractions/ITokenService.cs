using Quiz.Application.DTOs;

namespace Quiz.Application.Abstractions;

public interface ITokenService
{
    Task<Token> CreateAccessToken(string userId);
    
}