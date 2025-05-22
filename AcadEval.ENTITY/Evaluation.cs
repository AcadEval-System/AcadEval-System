using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EvaluatedUserId { get; set; }

        [Required]
        public int EvaluatorUserId { get; set; }

        [Required]
        public int SurveyId { get; set; }

        [Required]
        public double Score { get; set; }

        [Required]
        public DateTime EvaluationDate { get; set; }

        [ForeignKey("EvaluatedUserId")]
        public virtual User EvaluatedUser { get; set; }

        [ForeignKey("EvaluatorUserId")]
        public virtual User EvaluatorUser { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }

    }
}
