using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public bool IsRequired { get; set; } = false;

        [Required]
        public QuestionType Type {  get; set; }

        public virtual ICollection<Option> Options { get; set; }

    }
    public enum QuestionType
    {
        MultipleChoice = 1,
        //Respuesta libre en texto
        Open = 2,
        //Escala numerica para calificar algo
        NumericScale = 3
    }

}
