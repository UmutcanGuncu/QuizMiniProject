using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Handlers.Questions;
using Quiz.Domain.Entities;
using Quiz.Persistence.Contexts;
using Quiz.Persistence.Services;

namespace Quiz.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(AnswerQuestionCommandHandler).Assembly));
        services.AddDbContext<QuizDbContext>(options =>
            options.UseInMemoryDatabase("QuizDB"));
        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
    
            }) .AddEntityFrameworkStores<QuizDbContext>()
            .AddRoles<AppRole>()
            .AddDefaultTokenProviders();

        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        expires != null ? expires > DateTime.UtcNow : false,
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                };
            });
        return services;
    }
}