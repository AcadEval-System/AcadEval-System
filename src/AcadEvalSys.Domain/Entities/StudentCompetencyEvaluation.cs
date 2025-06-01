using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class StudentCompetencyEvaluation : BaseEntity
    {
        public string ProfessorCompetencyAssignmentId { get; set; }
        public string StudentId { get; set; } // References Student.UserId (which is User.Id)
        public string StudentName { get; set; }
        public string Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Comments { get; set; }
        public decimal? FinalScore { get; set; }

        public ProfessorCompetencyAssignment ProfessorCompetencyAssignment { get; set; }
        public Student Student { get; set; }
        public ICollection<QuestionResponse> QuestionResponses { get; set; }
    }
}
