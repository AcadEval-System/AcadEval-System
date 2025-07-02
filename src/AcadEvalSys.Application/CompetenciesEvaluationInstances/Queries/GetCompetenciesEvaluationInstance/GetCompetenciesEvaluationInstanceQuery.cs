using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using MediatR;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetCompetenciesEvaluationInstance;

public class GetCompetenciesEvaluationInstanceQuery(Guid id) : IRequest<CompetenciesEvaluationInstanceDetailDto>
{
    public Guid Id { get; } = id;
} 