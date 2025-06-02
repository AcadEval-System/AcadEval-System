using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string? Name { get; set; }
        public Guid? TechnicalCareerId { get; set; }
        public int Year { get; set; }
        public string? ProfessorId { get; set; }

        public virtual TechnicalCareer? TechnicalCareer { get; set; }
        public virtual Professor? Professor { get; set; }
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
