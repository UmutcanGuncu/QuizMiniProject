using System.Security.Claims;
using Quiz.Domain.Entities;

namespace Quiz.Application.Abstractions;

public interface IRoleService
{
    Task CreateRolesAsync();
    Task<bool> AssignUserToRoleAsync(AppUser user, string roleName);
    Task<IEnumerable<Claim>> GetUserClaimsAsync(string userId);
}