using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Domain.Entities;

namespace Quiz.Persistence.Contexts;

public class QuizDbContext : IdentityDbContext<AppUser,AppRole,Guid>
{ 
    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
    {
    }
    public DbSet<Question> Questions { get; set; }
   
}