using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Competency : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string TechnicalCareerId { get; set; }
        public int Year { get; set; }

        public TechnicalCareer TechnicalCareer { get; set; }
        public ICollection<FormQuestion> FormQuestions { get; set; }
        public ICollection<ProfessorCompetencyAssignment> ProfessorCompetencyAssignments { get; set; }
    }
}
