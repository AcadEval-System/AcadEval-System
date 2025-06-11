
using AcadEvalSys.Domain.Repositories;
using FluentValidation;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommandValidator : AbstractValidator<CreateCompetencyCommand>
{
    public CreateCompetencyCommandValidator(ICompetencyRepository competencyRepository)
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .MustAsync(async (name, cancellation) =>
                !await competencyRepository.ExistsByNameAsync(name))
            .WithMessage("A competency with this name already exists.");
        
        RuleFor(c => c.Description).MaximumLength(250);
        
        RuleFor(c => c.Type).IsInEnum();
    }
}
