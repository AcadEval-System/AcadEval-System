
using AcadEvalSys.Domain.Repositories;
using FluentValidation;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommandValidator : AbstractValidator<CreateCompetencyCommand>
{
    private readonly ICareerRepository careerRepository;

    public CreateCompetencyCommandValidator(ICareerRepository careerRepository)
    {
        this.careerRepository = careerRepository;

        RuleFor(c => c.Name).NotEmpty().MaximumLength(50).MinimumLength(3);
        RuleFor(c => c.Description).MaximumLength(250);
        RuleFor(c => c.Type).NotEmpty().MaximumLength(50).MinimumLength(3);
        RuleFor(c => c.CareerIds).NotEmpty();
        RuleFor(c => c.CareerIds).MustAsync(async (careerIds, cancellationToken) =>
{
    if (careerIds == null || careerIds.Count == 0)
        return true;

    var careers = await careerRepository.GetAllCareersAsync();
    return careerIds.All(id => careers.Any(c => c.Id == id));
}).WithMessage("One or more career IDs are invalid.");
    }
}
