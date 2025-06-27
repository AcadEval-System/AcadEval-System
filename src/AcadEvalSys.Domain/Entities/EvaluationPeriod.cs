using System;
using System.Collections.Generic;
using System.Linq;

namespace AcadEvalSys.Domain.Entities
{
    public class EvaluationPeriod : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
      
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
        public virtual ICollection<StudentEvaluationReport>? StudentEvaluationReports { get; set; } = new List<StudentEvaluationReport>();
        
        // Many-to-many relationship with TechnicalCareer
        public virtual ICollection<TechnicalCareer> TechnicalCareers { get; set; } = new List<TechnicalCareer>();
    }
}
