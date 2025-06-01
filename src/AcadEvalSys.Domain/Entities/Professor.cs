using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Professor
    {
        public string UserId { get; set; } // Foreign key to User.Id
        public string Phone { get; set; }

        // Navigation property to User
        public virtual User User { get; set; }

        // Collections
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<ProfessorCompetencyAssignment> ProfessorCompetencyAssignments { get; set; }
    }
}
