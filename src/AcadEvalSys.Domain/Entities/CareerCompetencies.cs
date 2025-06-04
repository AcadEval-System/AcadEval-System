using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities;



public class CareerCompetencies
{
    public Guid CareerId { get; set; }
    public TechnicalCareer Career { get; set; } = null!;

    public Guid CompetencyId { get; set; }
    public Competency Competency { get; set; } = null!;

    public CareerYear? CareerYear { get; set; }
}