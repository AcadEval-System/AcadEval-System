using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Authorization;
using AcadEvalSys.Infrastructure.Persistence;
using AcadEvalSys.Infrastructure.Repositories;
using AcadEvalSys.Infrastructure.Seeders;
using AcadEvalSys.Infrastructure.Services;
using AcadEvalSys.Application.Users.Services;

namespace AcadEvalSys.Infrastructure.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("AcadEvalDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'AcadEvalDb' not found.");
            }

            options.UseNpgsql(connectionString);

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
        
        // Configurar Identity con soporte para cookies
        services.AddIdentityApiEndpoints<User>(options =>
            {
                // Configuraciones de cookies de sesión
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                
                // Configuraciones de usuario
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Configurar cookies de autenticación
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(120);
            options.SlidingExpiration = true;
            
            // Configuración más permisiva para desarrollo/Postman
            options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            
            // Configurar rutas para manejo de autenticación
            options.LoginPath = "/api/identity/login";
            options.LogoutPath = "/api/identity/logout";
            options.AccessDeniedPath = "/api/identity/accessDenied";
            
            // Configurar para APIs (devolver códigos HTTP en lugar de redirecciones)
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });
        
        // Registrar servicios
        services.AddScoped<IDbSeeder, DbSeeder>();
        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IEvaluationPeriodRepository, EvaluationPeriodRepository>();
        services.AddScoped<IProfessorCompetencyAssignmentRepository, ProfessorCompetencyAssignmentRepository>();

    }
}