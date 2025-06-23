using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using FluentValidation;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;

public class CreateEvaluationPeriodCommandValidator : AbstractValidator<CreateEvaluationPeriodCommand>
{
    private static readonly string[] ValidYears = { "First", "Second", "Third", "Fourth", "Fifth" };

    public CreateEvaluationPeriodCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.PeriodFrom)
            .LessThan(x => x.PeriodTo)
            .WithMessage("PeriodFrom must be before PeriodTo");

        RuleFor(x => x.PeriodTo)
            .GreaterThan(x => x.PeriodFrom)
            .WithMessage("PeriodTo must be after PeriodFrom");

        RuleFor(x => x.CareerAssignments)
            .NotEmpty()
            .WithMessage("At least one career assignment is required");

        RuleForEach(x => x.CareerAssignments)
            .SetValidator(new CareerAssignmentDtoValidator());
    }
}