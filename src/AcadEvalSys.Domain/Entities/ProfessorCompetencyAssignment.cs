using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        public Guid? EvaluationId { get; set; }
        public string? ProfessorId { get; set; }
        public Guid? TechnicalCareerId { get; set; }
        public string? TechnicalCareerName { get; set; }
        public int Year { get; set; }
        public Guid? CompetencyId { get; set; }
        public string? CompetencyName { get; set; }
        public string? FormName { get; set; }
        public string? Status { get; set; }
        public DateTime? NotificationSentAt { get; set; }

        public virtual EvaluationPeriod? EvaluationPeriod { get; set; }
        public virtual Professor? Professor { get; set; }
        public virtual TechnicalCareer? TechnicalCareer { get; set; }
        public virtual Competency? Competency { get; set; }
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
    }
}
