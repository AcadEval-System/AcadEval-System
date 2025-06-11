using System;

namespace AcadEvalSys.Domain.Entities
{
    public class EvaluationPeriodCareer : BaseEntity
    {
        public Guid EvaluationPeriodId { get; set; }
        public Guid TechnicalCareerId { get; set; }
        
        // Navigation properties
        public virtual EvaluationPeriod? EvaluationPeriod { get; set; }
        public virtual TechnicalCareer? TechnicalCareer { get; set; }
    }
} 