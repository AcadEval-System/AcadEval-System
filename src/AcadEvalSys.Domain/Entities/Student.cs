using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class Student
    {
        public string? UserId { get; set; }
        public Guid? TechnicalCareerId { get; set; }
        public CareerYear CurrentYear { get; set; } // Default to first year
    
        // Navigation properties
        public virtual User? User { get; set; }
        
        public virtual TechnicalCareer? TechnicalCareer { get; set; }

        // Collections
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; } = new List<StudentSubject>();
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
        public virtual ICollection<StudentEvaluationReport>? EvaluationReports { get; set; } = new List<StudentEvaluationReport>();
    }
}
