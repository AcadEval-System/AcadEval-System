using FluentValidation;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CreateCompetencyAssignmentDtoValidator : AbstractValidator<CreateCompetencyAssignmentDto>
{
    public CreateCompetencyAssignmentDtoValidator()
    {
        RuleFor(x => x.CompetencyId)
            .NotEmpty()
            .WithMessage("El ID de la competencia es requerido");

        RuleFor(x => x.ProfessorId)
            .NotEmpty()
            .WithMessage("El ID del profesor es requerido");
    }
}