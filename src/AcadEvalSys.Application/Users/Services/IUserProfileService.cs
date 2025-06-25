using AcadEvalSys.Application.Users.Dtos;

namespace AcadEvalSys.Application.Users.Services;

public interface IUserProfileService
{
    Task<StudentDetailsDto?> GetStudentDetailsAsync(string userId, CancellationToken cancellationToken = default);
    Task<ProfessorDetailsDto?> GetProfessorDetailsAsync(string userId, CancellationToken cancellationToken = default);
    Task<CoordinatorDetailsDto?> GetCoordinatorDetailsAsync(string userId, CancellationToken cancellationToken = default);
} 