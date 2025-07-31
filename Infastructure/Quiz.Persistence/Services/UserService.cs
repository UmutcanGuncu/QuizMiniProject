using Microsoft.AspNetCore.Identity;
using Quiz.Application.Abstractions;
using Quiz.Domain.Entities;

namespace Quiz.Persistence.Services;

public class UserService(UserManager<AppUser>  userManager)  : IUserService
{
    public async Task<AppUser?> FindUserAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }
}