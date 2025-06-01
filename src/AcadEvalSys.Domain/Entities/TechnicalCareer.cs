using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class TechnicalCareer : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Competency> Competencies { get; set; }
        public ICollection<ProfessorCompetencyAssignment> ProfessorCompetencyAssignments { get; set; }
    }
}
