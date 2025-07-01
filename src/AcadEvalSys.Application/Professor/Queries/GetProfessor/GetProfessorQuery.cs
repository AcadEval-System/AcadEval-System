using AcadEvalSys.Application.Professor.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Professor.Queries.GetProfessor;

public class GetProfessorQuery(Guid id) : IRequest<ProfessorDto>
{
    public Guid Id { get;  } = id;
}