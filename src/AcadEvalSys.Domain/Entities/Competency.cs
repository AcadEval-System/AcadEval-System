using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class Competency : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CompetencyType Type { get; set; }

        public ICollection<CareerCompetencies>? CareerCompetencies { get; set; }
        public virtual ICollection<FormQuestion>? FormQuestions { get; set; } = new List<FormQuestion>();
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
    }
}
