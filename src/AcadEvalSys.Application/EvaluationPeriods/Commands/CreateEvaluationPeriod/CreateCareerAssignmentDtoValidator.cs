using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using AcadEvalSys.Domain.Enums;
using FluentValidation;

namespace AcadEvalSys.Application.Commands.CreateEvaluationPeriod;

public class CreateCareerAssignmentDtoValidator : AbstractValidator<CreateCareerAssignmentDto>
{
    public CreateCareerAssignmentDtoValidator()
    {
        RuleFor(x => x.TechnicalCareerId)
            .NotEmpty()
            .WithMessage("TechnicalCareerId is required");

        RuleFor(x => x.AssignmentsByYear)
            .NotEmpty()
            .WithMessage("At least one year assignment is required");

        RuleForEach(x => x.AssignmentsByYear.Values.SelectMany(list => list))
            .SetValidator(new CreateCompetencyAssignmentDtoValidator());
    }
}