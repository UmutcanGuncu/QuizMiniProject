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
        var roles = new [] {"Admin", "User"}; // admin ve user rol dizisi tanımlanır
        foreach (var role in roles) // dizi içindeki elemanlar sırayla gezikir
        {
            // ilgili rol bilgisi yoksa rol ekleme işlemi gerçekleştirilir
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new AppRole { Name = role }); 
        }
    }

    public async Task<bool> AssignUserToRoleAsync(AppUser user, string roleName)
    {
        if (user == null)
            return false; // kullanıcı bilgisi gelmezse false döndürülür
        // verilen rol bilgisi yoksa oluşturur
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new AppRole { Name = roleName }); 
        }
        // kullanıcının rollerini liste şeklibde döndürür
        var userRoles = await userManager.GetRolesAsync(user); 
        if(userRoles.Contains(roleName)) // döndürülen listenin içerisinde roleName rolu varsa true döndürür
            return true;
        // kullanıcının rolleri arasında parametreden gelen rolName rolü olnadığından dolayı rol kullanıcıya atanır
        var result = await userManager.AddToRoleAsync(user, roleName); 
        return result.Succeeded;
    }

    public async Task<IEnumerable<Claim>> GetUserClaimsAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId); //kullanıcı bilgisi aranıe
        if (user != null)
        {
            var roles = await userManager.GetRolesAsync(user); // kullanıcının rolleri listelenir
            //IEnurable<Claim> formatına dönüştürülür
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            return roleClaims; 
        }
        return new List<Claim>(); //boş liste geri döndürülür
    }
}