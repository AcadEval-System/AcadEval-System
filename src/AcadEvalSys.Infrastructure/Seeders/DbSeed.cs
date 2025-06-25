

using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AcadEvalSys.Infrastructure.Seeders;

internal class DbSeeder(ApplicationDbContext dbContext, UserManager<User> userManager) : IDbSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            // Primero creamos roles si no existen
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            // Después creamos el usuario administrador
            var adminId = await EnsureAdminUser();

            // Finalmente creamos restaurantes si no existen
            if (!dbContext.TechnicalCareers.Any())
            {
                var careers = GetCareers();
                dbContext.TechnicalCareers.AddRange(careers);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task<string> EnsureAdminUser()
    {
        const string adminEmail = "admin@itec.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, "Administrator1390_");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }
        }

        return adminUser.Id;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
        [
            new(UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpper() },
            new(UserRoles.Student) { NormalizedName = UserRoles.Student.ToUpper() },
            new(UserRoles.Professor) { NormalizedName = UserRoles.Professor.ToUpper() },
            new(UserRoles.Coordinator) { NormalizedName = UserRoles.Coordinator.ToUpper() }
        ];

        return roles;
    }

    private IEnumerable<TechnicalCareer> GetCareers()
    {
        List<TechnicalCareer> careers =
        [
            new() { Name = "Desarrollo de Software"},
            new() { Name = "Logística"},   
            new() { Name = "Mantenimiento Industrial"},
            new() { Name = "Gestión Industrial"},
            new() { Name = "Seguridad, Higiene y Medio Ambiente"},
            new() { Name = "Gestión de Energías Renovables"},
        ];

        return careers;
    }
}