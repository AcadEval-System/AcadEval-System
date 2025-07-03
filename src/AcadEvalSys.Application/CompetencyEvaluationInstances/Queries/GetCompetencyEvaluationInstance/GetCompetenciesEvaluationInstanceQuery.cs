using AcadEvalSys.Application.CompetencyEvaluationInstances.Dtos;
using MediatR;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetCompetencyEvaluationInstance;

public class GetCompetencyEvaluationInstanceQuery(Guid id) : IRequest<CompetencyEvaluationInstanceDetailDto>
{
    public Guid Id { get; } = id;
}