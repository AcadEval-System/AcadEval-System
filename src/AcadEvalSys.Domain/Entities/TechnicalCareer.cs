using System;
using System.Collections.Generic;
using System.Linq;

namespace AcadEvalSys.Domain.Entities
{
    public class TechnicalCareer : BaseEntity
    {
        public string? Name { get; set; }
        
        // Navigation properties
        public virtual ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
        public virtual ICollection<Coordinator>? Coordinators { get; set; } = new List<Coordinator>();
        public virtual ICollection<Competency> Competencies { get; set; } = new List<Competency>();
        public virtual ICollection<Student>? Students { get; set; } = new List<Student>();
        
        // Many-to-many relationship with EvaluationPeriod
        public virtual ICollection<EvaluationPeriod> EvaluationPeriods { get; set; } = new List<EvaluationPeriod>();
    }
}
