using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        public Guid CompetencyEvaluationInstanceId { get; set; }
        public Guid CompetencyId { get; set; }
        public Guid SubjectId { get; set; }

        public virtual CompetencyEvaluationInstance? CompetencyEvaluationInstance { get; set; }
        public virtual Competency? Competency { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<StudentCompetencyAssessment>? StudentCompetencyAssessments { get; set; } = new List<StudentCompetencyAssessment>();
    }
}
