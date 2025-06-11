using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        // Required properties - no nullable
        public Guid EvaluationPeriodId { get; set; }  // Renamed from EvaluationId for clarity
        public string ProfessorId { get; set; } = string.Empty;
        public Guid TechnicalCareerId { get; set; }
        public int Year { get; set; }
        public Guid CompetencyId { get; set; }
        
        // Optional properties
        public string? FormName { get; set; }
        public string? Status { get; set; }
        public DateTime? NotificationSentAt { get; set; }

        // Navigation properties
        public virtual EvaluationPeriod? EvaluationPeriod { get; set; }
        public virtual Professor? Professor { get; set; }
        public virtual TechnicalCareer? TechnicalCareer { get; set; }
        public virtual Competency? Competency { get; set; }
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
        
        // Computed properties for convenience
        public string? TechnicalCareerName => TechnicalCareer?.Name;
        public string? CompetencyName => Competency?.Name;
    }
}
