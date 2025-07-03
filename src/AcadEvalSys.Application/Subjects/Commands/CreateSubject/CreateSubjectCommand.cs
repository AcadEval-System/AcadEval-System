using AcadEvalSys.Domain.Enums;
using MediatR;

namespace AcadEvalSys.Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommand: IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid TechnicalCareerId { get; set; }
    public CareerYear Year { get; set; }
    public string ProfessorId { get; set; }
}