using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class ProfessorRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : IProfessorRepository
{
    public async Task<Professor?> GetProfessorByIdAsync(string professorId)
    {
        return await dbContext.Professors
            .Include(p => p.User)
            .Include(p => p.Subjects!)
                .ThenInclude(s => s.TechnicalCareer)
            .FirstOrDefaultAsync(p => p.UserId == professorId);
    }

    public async Task<bool> ExistsAsync(string professorId)
    {
        return await dbContext.Professors
            .AnyAsync(p => p.UserId == professorId);
    }

    public async Task<bool> IsActiveAsync(string professorId)
    {
        // Verificar que existe en la tabla Professors
        var professorExists = await ExistsAsync(professorId);
        if (!professorExists) return false;

        // Verificar el estado del usuario subyacente
        var user = await userManager.FindByIdAsync(professorId);
        if (user == null) return false;

        // Verificar si el usuario no está bloqueado
        var isLockedOut = await userManager.IsLockedOutAsync(user);
        if (isLockedOut) return false;

        // Verificar si el email está confirmado (opcional, según tus reglas de negocio)
        if (!user.EmailConfirmed) return false;

        return true;
    }

    public async Task<IEnumerable<Professor>> GetAllProfessorsAsync()
    {
        return await dbContext.Professors
            .Include(p => p.User)
            .Include(p => p.Subjects!)
                .ThenInclude(s => s.TechnicalCareer)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByProfessorAsync(string professorId)
    {
        return await dbContext.Subjects
            .Include(s => s.TechnicalCareer)
            .Include(s => s.Professor!)
                .ThenInclude(p => p.User)
            .Where(s => s.ProfessorId == professorId && s.IsActive)
            .ToListAsync();
    }
}