using System;
using System.Collections.Generic;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Domain.Entities
{
    public class StudentCompetencyEvaluation : BaseEntity
    {
        public string? StudentId { get; set; }
        public Guid ProfessorCompetencyAssignmentId { get; set; }
        public CareerYear CareerYear { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Comments { get; set; }
        public decimal? FinalScore { get; set; }
        
        // New property for rubric-based evaluation
        public CompetencyLevel? CompetencyLevel { get; set; }

        // Navigation properties
        public virtual Student? Student { get; set; }
        public virtual ProfessorCompetencyAssignment? ProfessorCompetencyAssignment { get; set; }
        public virtual ICollection<QuestionResponse>? QuestionResponses { get; set; } = new List<QuestionResponse>();

        // Computed property to access Competency through the assignment
        public Competency? Competency => ProfessorCompetencyAssignment?.Competency;
        public Guid? CompetencyId => ProfessorCompetencyAssignment?.CompetencyId;
    }
}
