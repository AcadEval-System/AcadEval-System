using FluentValidation;

namespace AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;

public class UpdateCompetencyCommandValidator : AbstractValidator<UpdateCompetencyCommand>
{
    public UpdateCompetencyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Competency description is required.")
            .MaximumLength(500)
            .WithMessage("Competency description must not exceed 500 characters.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Invalid competency type.");
    }
}