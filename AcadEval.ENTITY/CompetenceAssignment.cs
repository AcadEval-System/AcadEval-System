using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadEval.ENTITY
{
    public class CompetenceAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CompetenceId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [ForeignKey("CompetenceId")]
        public virtual Competence Competence { get; set; }

        [ForeignKey("TeacherId")]
        public virtual User Teacher { get; set; }
    }
}
