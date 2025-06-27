using MediatR;

namespace AcadEvalSys.Application.Career.Commands.CreateCareer;

public class CreateCareerCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}