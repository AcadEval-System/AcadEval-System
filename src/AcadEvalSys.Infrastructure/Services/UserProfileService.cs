using AcadEvalSys.Application.Users.Dtos;
using AcadEvalSys.Application.Users.Services;
using AcadEvalSys.Infrastructure.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Services;

public class UserProfileService(ApplicationDbContext dbContext, IMapper mapper) : IUserProfileService
{
    public async Task<StudentDetailsDto?> GetStudentDetailsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var student = await dbContext.Students
            .Include(s => s.TechnicalCareer)
            .Include(s => s.StudentSubjects!)
                .ThenInclude(ss => ss.Subject!)
                    .ThenInclude(s => s.TechnicalCareer)
            .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);

        if (student is null) return null;

        var result = mapper.Map<StudentDetailsDto>(student);
        
        // Mapear las materias del estudiante
        result.Subjects = student.StudentSubjects?.Select(ss => new SubjectDetailsDto
        {
            Id = ss.Subject!.Id,
            Name = ss.Subject.Name!,
            Year = ss.Subject.Year,
            TechnicalCareerName = ss.Subject.TechnicalCareer?.Name
        }) ?? Enumerable.Empty<SubjectDetailsDto>();

        return result;
    }

    public async Task<ProfessorDetailsDto?> GetProfessorDetailsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var professor = await dbContext.Professors
            .Include(p => p.Subjects!)
                .ThenInclude(s => s.TechnicalCareer)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

        if (professor is null) return null;

        var result = mapper.Map<ProfessorDetailsDto>(professor);
        
        // Mapear las materias del profesor
        result.Subjects = professor.Subjects?.Select(s => new SubjectDetailsDto
        {
            Id = s.Id,
            Name = s.Name!,
            Year = s.Year,
            TechnicalCareerName = s.TechnicalCareer?.Name
        }) ?? Enumerable.Empty<SubjectDetailsDto>();

        return result;
    }

    public async Task<CoordinatorDetailsDto?> GetCoordinatorDetailsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var coordinator = await dbContext.Coordinators
            .Include(c => c.TechnicalCareer)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (coordinator is null) return null;

        return mapper.Map<CoordinatorDetailsDto>(coordinator);
    }
} 