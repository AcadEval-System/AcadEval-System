using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICareerRepository
{
    Task<Guid> Create(TechnicalCareer entity);
    Task Update();
    Task Delete(TechnicalCareer entity);

    Task<IEnumerable<TechnicalCareer>> GetAllCareersAsync();
    Task<TechnicalCareer?> GetCareerByIdAsync(Guid id);
}