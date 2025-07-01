using FluentValidation;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CreateCompetencyAssignmentDtoValidator : AbstractValidator<CreateCompetencyAssignmentDto>
{
    public CreateCompetencyAssignmentDtoValidator()
    {
        RuleFor(x => x.CompetencyId)
            .NotEmpty()
            .WithMessage("El ID de la competencia es requerido");

        RuleFor(x => x.SubjectId)
            .NotEmpty()
            .WithMessage("El ID de la asignatura es requerido");
    }
}