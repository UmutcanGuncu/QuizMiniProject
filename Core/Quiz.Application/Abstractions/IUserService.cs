using Quiz.Domain.Entities;

namespace Quiz.Application.Abstractions;

public interface IUserService
{
    Task<AppUser?> FindUserAsync(string userId);
}