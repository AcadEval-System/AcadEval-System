using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class CompetenceQuestion
    {
        //Cumple la funcion de tabla intermedia que relaciona las competencias con las preguntas, para permitir que una pregunta pueda estar asociada a varias competencias y viceversa.
        //Hacemos uso de PK compuesta entre CompetenceId y QuestionId.

        [Required]
        public int CompetenceId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("CompetenciaId")]
        public virtual Competence Competence { get; set; }

        [ForeignKey("PreguntaId")]
        public virtual Question Question { get; set; }
    }
}
