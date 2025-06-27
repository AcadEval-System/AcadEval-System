namespace AcadEvalSys.Application.Career.Dtos;

public record CareerDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}