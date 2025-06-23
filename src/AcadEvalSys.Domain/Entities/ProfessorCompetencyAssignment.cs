using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        public Guid EvaluationPeriodId { get; set; }
        public Guid CompetencyId { get; set; }
        public string ProfessorId { get; set; } = string.Empty;
        public Guid TechnicalCareerId { get; set; }
        public CareerYear Year { get; set; }
        
        public virtual EvaluationPeriod? EvaluationPeriod { get; set; }
        public virtual Competency? Competency { get; set; }
        public virtual Professor? Professor { get; set; }
        public virtual TechnicalCareer? TechnicalCareer { get; set; }
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
    }
}
