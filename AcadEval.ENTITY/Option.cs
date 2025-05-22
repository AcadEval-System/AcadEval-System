using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class Option
    {
        //Esta tabla se encarga de manejar las opciones de respuesta apra preguntas multiple-choice, una pregunta puede tener muchas opciones, pero cada opcion pertenece a una sola pregunta.
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        [Required]
        [StringLength(255)]
        public string Text {  get; set; }

    }
}
