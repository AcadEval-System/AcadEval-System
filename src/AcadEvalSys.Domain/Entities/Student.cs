using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Student
    {
        public string? UserId { get; set; }

        // Navigation property to User
        public virtual User? User { get; set; }

        // Collections
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; } = new List<StudentSubject>();
        public virtual ICollection<StudentCompetencyEvaluation>? StudentCompetencyEvaluations { get; set; } = new List<StudentCompetencyEvaluation>();
    }
}
