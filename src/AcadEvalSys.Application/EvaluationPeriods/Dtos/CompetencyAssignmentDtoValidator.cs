using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using FluentValidation;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CompetencyAssignmentDtoValidator : AbstractValidator<CompetencyAssignmentDto>
{
    public CompetencyAssignmentDtoValidator()
    {
        RuleFor(x => x.CompetencyId)
            .NotEmpty()
            .WithMessage("CompetencyId is required");

        RuleFor(x => x.ProfessorId)
            .NotEmpty()
            .WithMessage("ProfessorId is required");
    }
}