using FluentValidation;

namespace AcadEvalSys.Application.Professor.Commands.CreateProfessor;

public class CreateProfessorCommandValidator : AbstractValidator<CreateProfessorCommand>
{
    public CreateProfessorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(c => c.Email)
            .MaximumLength(250)
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100)
            .WithMessage("Password must be between 6 and 100 characters long");
    }
}