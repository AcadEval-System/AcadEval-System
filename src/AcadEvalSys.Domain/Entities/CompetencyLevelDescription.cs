using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class CompetencyLevelDescription : BaseEntity
    {
        public Guid CompetencyId { get; set; }
        public CompetencyLevel Level { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property
        public virtual Competency? Competency { get; set; }
    }
} 