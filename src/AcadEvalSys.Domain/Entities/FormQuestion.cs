using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities
{
    public class FormQuestion : BaseEntity
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public string CompetencyId { get; set; }

        public Competency Competency { get; set; }
        public ICollection<QuestionResponse> QuestionResponses { get; set; }
    }
}
