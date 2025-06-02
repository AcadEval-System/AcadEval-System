using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class EvaluationPeriod : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public bool NotifyStart { get; set; }
        public bool SendReminders { get; set; }
        public bool NotifyClose { get; set; }
        public string? ReminderFrequency { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<ProfessorCompetencyAssignment>? ProfessorCompetencyAssignments { get; set; } = new List<ProfessorCompetencyAssignment>();
    }
}
