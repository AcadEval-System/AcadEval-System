

using AcadEvalSys.Application.Competencies.Queries.GetCompetency;
using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Enums;
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

            if (!dbContext.TechnicalCareers.Any())
            {
                var careers = GetCareers();
                dbContext.TechnicalCareers.AddRange(careers);
                await dbContext.SaveChangesAsync();
            }

            // Competencies
            if (!dbContext.Competencies.Any())
            {
                var competencies = GetCompetencies();
                dbContext.Competencies.AddRange(competencies);
                await dbContext.SaveChangesAsync(); // Guardar para obtener los IDs generados

                // Obtener las competencias desde la base con IDs generados
                var insertedCompetencies = await dbContext.Competencies.ToListAsync();

                var descriptions = GetCompetencyLevelDescriptions(insertedCompetencies);
                dbContext.CompetencyLevelDescriptions.AddRange(descriptions);
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
            new(UserRoles.User) { NormalizedName = UserRoles.User.ToUpper() },
            new(UserRoles.Student) { NormalizedName = UserRoles.Student.ToUpper() },
            new(UserRoles.Professor) { NormalizedName = UserRoles.Professor.ToUpper() }
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
    
    private IEnumerable<Competency> GetCompetencies()
    {
        List<Competency> competencies = new()
        {
            new() 
            { 
                Name = "Liderazgo", 
                Description = "Capacidad de liderar equipos, motivar y guiar con visión.", 
                Type = CompetencyType.Soft 
            },
            new() 
            { 
                Name = "Comunicación Efectiva", 
                Description = "Habilidad para transmitir ideas de manera clara y persuasiva, adaptándose al contexto.", 
                Type = CompetencyType.Soft
            },
            new() 
            { 
                Name = "Gestión Emocional", 
                Description = "Capacidad de manejar las propias emociones y comprender las de los demás en entornos laborales.", 
                Type =CompetencyType.Soft 
            },
            new() 
            { 
                Name = "Proactividad", 
                Description = "Iniciativa para anticiparse a problemas y proponer mejoras.", 
                Type =CompetencyType.Soft 
            },
            new() 
            { 
                Name = "Trabajo en Equipo", 
                Description = "Habilidad para colaborar eficazmente, gestionar conflictos y potenciar la sinergia grupal.", 
                Type =CompetencyType.Soft 
            }
        };
        return competencies;
    }

    private IEnumerable<CompetencyLevelDescription> GetCompetencyLevelDescriptions(IEnumerable<Competency> competencies)
    {
        var descriptions = new List<CompetencyLevelDescription>();

        foreach (var competency in competencies)
        {
            descriptions.Add(new CompetencyLevelDescription
            {
                CompetencyId = competency.Id,
                Level = CompetencyLevel.Inicial,
                Description = GetLevel1Description(competency.Name)
            });

            descriptions.Add(new CompetencyLevelDescription
            {
                CompetencyId = competency.Id,
                Level = CompetencyLevel.Intermedio,
                Description = GetLevel2Description(competency.Name)
            });

            descriptions.Add(new CompetencyLevelDescription
            {
                CompetencyId = competency.Id,
                Level = CompetencyLevel.Avanzado,
                Description = GetLevel3Description(competency.Name)
            });

            descriptions.Add(new CompetencyLevelDescription
            {
                CompetencyId = competency.Id,
                Level = CompetencyLevel.Excelente,
                Description = GetLevel4Description(competency.Name)
            });
        }

        return descriptions;
    }

    
    private string GetLevel1Description(string competencyName)
{
    return competencyName switch
    {
        "Liderazgo" => "Participa solo cuando se le indica y evita tomar decisiones.",
        "Comunicación Efectiva" => "Se expresa con dificultad y su mensaje suele ser confuso o incompleto.",
        "Gestión Emocional" => "Reacciona impulsivamente y evita enfrentar situaciones difíciles.",
        "Proactividad" => "Espera instrucciones para actuar y no anticipa problemas.",
        "Trabajo en Equipo" => "Cumple su parte sin integrarse ni coordinar con el grupo.",
        _ => "Descripción no definida."
    };
}

private string GetLevel2Description(string competencyName)
{
    return competencyName switch
    {
        "Liderazgo" => "Asume tareas de coordinación en situaciones simples y motiva ocasionalmente.",
        "Comunicación Efectiva" => "Comunica con mayor claridad y adapta su mensaje según el contexto.",
        "Gestión Emocional" => "Controla sus emociones en situaciones tensas y expresa sus ideas con mayor claridad.",
        "Proactividad" => "Toma iniciativa en tareas conocidas y propone mejoras puntuales.",
        "Trabajo en Equipo" => "Colabora activamente, escucha y negocia en situaciones de desacuerdo.",
        _ => "Descripción no definida."
    };
}

private string GetLevel3Description(string competencyName)
{
    return competencyName switch
    {
        "Liderazgo" => "Lidera con planificación, distribuye tareas y resuelve conflictos con eficacia.",
        "Comunicación Efectiva" => "Se comunica con seguridad, escucha activamente y persuade con argumentos sólidos.",
        "Gestión Emocional" => "Mantiene la calma, regula el clima grupal y actúa con empatía ante el conflicto.",
        "Proactividad" => "Actúa con autonomía, detecta oportunidades y propone soluciones innovadoras.",
        "Trabajo en Equipo" => "Promueve la participación equitativa, gestiona conflictos y fortalece la cohesión.",
        _ => "Descripción no definida."
    };
}

private string GetLevel4Description(string competencyName)
{
    return competencyName switch
    {
        "Liderazgo" => "Lidera con visión, empodera al equipo y transforma dinámicas grupales.",
        "Comunicación Efectiva" => "Domina distintos registros comunicativos, influye estratégicamente y gestiona conversaciones complejas.",
        "Gestión Emocional" => "Lidera con inteligencia emocional, anticipa tensiones y promueve el bienestar colectivo.",
        "Proactividad" => "Lidera mejoras continuas, anticipa desafíos y moviliza al grupo hacia la acción.",
        "Trabajo en Equipo" => "Fomenta equipos de alto rendimiento, media con eficacia y potencia la sinergia grupal.",
        _ => "Descripción no definida."
    };
}

}