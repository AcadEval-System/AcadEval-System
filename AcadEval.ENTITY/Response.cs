using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class Response
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        //Valor porque no sabemos si es texto, numero, opcion seleccionada, etc.
        public string Value{ get; set; }

        [Required]
        public DateTime SubmittedAt { get; set; }  // Fecha de envío

        [Required]
        // Nullable: solo se completa si es opción múltiple
        public int? SelectedOption { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
