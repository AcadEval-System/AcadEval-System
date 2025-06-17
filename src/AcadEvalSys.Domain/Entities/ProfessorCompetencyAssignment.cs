using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        public Guid EvaluationPeriodCareer { get; set; }  
        public Guid CompetencyId { get; set; }
        public string ProfessorId { get; set; } = string.Empty;
        public Guid TechnicalCareerId { get; set; }
        public CareerYear Year { get; set; }
        
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
        
    }
}
