using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Quiz.Application.Abstractions;
using Quiz.Domain.Entities;

namespace Quiz.Persistence.Services;

public class UserService(UserManager<AppUser>  userManager, IHttpContextAccessor httpContextAccessor)  : IUserService
{
    public async Task<AppUser?> FindUserAsync(string userId)
    {
        // Appuser sınıfı şeklinde bulunan kullanıcı bilgisini geri döndürür
        return await userManager.FindByIdAsync(userId);
    }

    public async Task<int?> UpdateUserPointAsync(int point)
    {
        //aynı sınıfta bulunmakta olan GetCurrentUsername metodu çağrılarak kullanıcı adı bilgisi alınır
        var username = GetCurrentUsername();
        var user = await userManager.FindByEmailAsync(username); // kullanıcı adı bilgisi ile kullanıcı sorgulanır
        if(user is null) // sorgu neticesinde kullanıcı bulunamazsa 0 değeri döndürülür
            return 0;
        user.Points += point; // gelen puan bilgisi eklenir
        await userManager.UpdateAsync(user); // güncelleme işlemi gerçekleştirilir
        return user.Points; // toplam puan bilgisi geri döndürülür
    }

    public string? GetCurrentUsername()
    {
        return httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;  // httpContextAccessor sayesinde giirş yapmış olan kullanıcı adı bilgisi geri döndürülür
        
    }
}