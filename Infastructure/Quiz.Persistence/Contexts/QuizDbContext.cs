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
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.Entries().ToList().ForEach(entry =>
        {
            if (entry.Entity is AppUser appUser)
            {
                if (entry.State == EntityState.Added)
                    appUser.Points = 0;
                
            }
        });
        return base.SaveChangesAsync(cancellationToken);
    }
}