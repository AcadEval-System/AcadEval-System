using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class StudentCompetencyEvaluation : BaseEntity
    {
        public string? StudentId { get; set; }
        public Guid ProfessorCompetencyAssignmentId { get; set; }
        public Guid SubjectId { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Comments { get; set; }
        public decimal? FinalScore { get; set; }
        
        public CompetencyLevel? CompetencyLevel { get; set; }

        public virtual Student? Student { get; set; }
        public virtual ProfessorCompetencyAssignment? ProfessorCompetencyAssignment { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<QuestionResponse>? QuestionResponses { get; set; } = new List<QuestionResponse>();
    }
}
