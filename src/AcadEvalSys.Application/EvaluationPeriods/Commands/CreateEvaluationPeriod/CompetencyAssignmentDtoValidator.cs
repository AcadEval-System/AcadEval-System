using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using FluentValidation;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;

public class CompetencyAssignmentDtoValidator : AbstractValidator<CompetencyAssignmentDto>
{
    public CompetencyAssignmentDtoValidator()
    {
        RuleFor(x => x.CompetencyId)
            .NotEmpty()
            .WithMessage("El ID de la competencia es requerido");

        RuleFor(x => x.ProfessorId)
            .NotEmpty()
            .WithMessage("El ID del profesor es requerido");
    }
}