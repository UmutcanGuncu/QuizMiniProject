using Quiz.Application.DTOs;

namespace Quiz.Application.Abstractions;

public interface ITokenService
{
    Task<Token> CreateAccessTokenAsync(string userId);
    
}