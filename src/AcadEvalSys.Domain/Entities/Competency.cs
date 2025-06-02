using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Competency : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Guid? TechnicalCareerId { get; set; }

        public virtual TechnicalCareer? TechnicalCareer { get; set; }
        public virtual ICollection<FormQuestion>? FormQuestions { get; set; } = new List<FormQuestion>();
        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
    }
}
