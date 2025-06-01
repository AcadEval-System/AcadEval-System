using System;

namespace AcadEvalSys.Domain.Entities
{
    public class QuestionResponse : BaseEntity
    {
        public string FormQuestionId { get; set; }
        public string StudentCompetencyEvaluationId { get; set; }
        public int Value { get; set; }
        public string? Comments { get; set; }

        // Navigation properties
        public virtual FormQuestion FormQuestion { get; set; }
        public virtual StudentCompetencyEvaluation StudentCompetencyEvaluation { get; set; }
    }
}
