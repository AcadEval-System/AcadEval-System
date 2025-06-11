using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Professor
    {
        public string? UserId { get; set; }
        public string? Phone { get; set; }

        // Navigation property to User
        public virtual User? User { get; set; }

        // Collections
        public virtual ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
    }
}
