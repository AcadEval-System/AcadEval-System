using AcadEvalSys.Application.Competencies.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Competencies.Queries.GetCompetency;

public class GetCompetencyQuery(Guid id)  : IRequest<CompetencyDto>
{
    public Guid Id { get;  } = id;
}