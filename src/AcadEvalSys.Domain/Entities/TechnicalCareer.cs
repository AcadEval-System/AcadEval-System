using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class TechnicalCareer : BaseEntity
    {
        public string? Name { get; set; }
        
        public virtual ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
        public virtual ICollection<Competency>? Competencies { get; set; } = new List<Competency>();
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
        public virtual ICollection<Coordinator>? Coordinators { get; set; } = new List<Coordinator>();
    }
}
