using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Quiz.Application.Abstractions;
using Quiz.Domain.Entities;

namespace Quiz.Persistence.Services;

// rol oluşturma, kullanıcıya rol atama ve kullanıcıların rollerinin IEnurable<Claim> şeklinde gönderme işlemleri gerçekleştirildi
public class RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : IRoleService
{
    public async Task CreateRolesAsync()
    {
        var roles = new [] {"Admin", "User"};
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new AppRole { Name = role }); 
        }
    }

    public async Task<bool> AssignUserToRoleAsync(AppUser user, string roleName)
    {
        if (user == null)
            return false;
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new AppRole { Name = roleName });
        }
        var userRoles = await userManager.GetRolesAsync(user);
        if(userRoles.Contains(roleName))
            return true;
        var result = await userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }

    public async Task<IEnumerable<Claim>> GetUserClaimsAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            return roleClaims;
        }
        return new List<Claim>();
    }
}