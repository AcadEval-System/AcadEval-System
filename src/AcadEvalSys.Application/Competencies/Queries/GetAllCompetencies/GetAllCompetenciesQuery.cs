using AcadEvalSys.Application.Competencies.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Competencies.Queries.GetAllCompetencies;

public class GetAllCompetenciesQuery : IRequest<IEnumerable<CompetencyDto>>
{

}