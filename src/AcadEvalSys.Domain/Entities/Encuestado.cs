using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class Encuestado
{
    public int Id { get; set; }

    public int EncuestaId { get; set; }
    public virtual Encuesta? Encuesta { get; set; }

    public string UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public string Estado { get; set; } = string.Empty;
    public DateTime? FechaCompletado { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<RespuestaEncuesta> RespuestasEncuesta { get; set; } = new List<RespuestaEncuesta>();
}