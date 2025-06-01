using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class ProfessorCompetencyAssignment : BaseEntity
    {
        public string EvaluationId { get; set; }
        public string ProfessorId { get; set; } // References Professor.UserId (which is User.Id)
        public string TechnicalCareerId { get; set; }
        public string TechnicalCareerName { get; set; }
        public int Year { get; set; }
        public string CompetencyId { get; set; }
        public string CompetencyName { get; set; }
        public string FormName { get; set; }
        public string Status { get; set; }
        public DateTime? NotificationSentAt { get; set; }

        public EvaluationPeriod EvaluationPeriod { get; set; }
        public Professor Professor { get; set; }
        public TechnicalCareer TechnicalCareer { get; set; }
        public Competency Competency { get; set; }
        public ICollection<StudentCompetencyEvaluation> StudentCompetencyEvaluations { get; set; }
    }
}
