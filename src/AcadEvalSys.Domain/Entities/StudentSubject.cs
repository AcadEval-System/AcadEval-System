using System;

namespace AcadEvalSys.Domain.Entities
{
    public class StudentSubject : BaseEntity
    {
        public string StudentId { get; set; } // References Student.UserId (which is User.Id)
        public string SubjectId { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
