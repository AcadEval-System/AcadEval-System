using System;

namespace AcadEvalSys.Domain.Entities
{
    public class StudentEvaluationReport : BaseEntity
    {
        public string StudentId { get; set; } = string.Empty;
        public Guid CompetenciesEvaluationInstanceId { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        
        // Navigation properties
        public virtual Student? Student { get; set; }
        public virtual CompetencyEvaluationInstance? CompetencyEvaluationInstance { get; set; }
    }
} 