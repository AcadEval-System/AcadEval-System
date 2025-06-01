using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string TechnicalCareerId { get; set; }
        public int Year { get; set; }
        public string ProfessorId { get; set; } // References Professor.UserId (which is User.Id)

        public TechnicalCareer TechnicalCareer { get; set; }
        public Professor Professor { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
