using Microsoft.AspNetCore.Identity;

namespace Quiz.Domain.Entities;

public class AppUser :IdentityUser<Guid>
{
    public string NameSurname { get; set; }
    public int? Points { get; set; }
}