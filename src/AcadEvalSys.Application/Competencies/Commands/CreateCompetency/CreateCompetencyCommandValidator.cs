using FluentValidation;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommandValidator : AbstractValidator<CreateCompetencyCommand>
{
    public CreateCompetencyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(c => c.Description)
            .MaximumLength(250);
        
        RuleFor(c => c.Type)
            .IsInEnum();
    }
}
