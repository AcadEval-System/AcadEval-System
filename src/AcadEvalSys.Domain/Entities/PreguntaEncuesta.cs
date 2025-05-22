using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class PreguntaEncuesta
{
    public int Id { get; set; }

    public int EncuestaId { get; set; }
    public virtual Encuesta? Encuesta { get; set; }

    public string TextoPregunta { get; set; } = string.Empty;
    public string TipoPregunta { get; set; } = string.Empty;
    public string? Opciones { get; set; }
    public int OrdenEnEncuesta { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<RespuestaEncuesta> RespuestasEncuesta { get; set; } = new List<RespuestaEncuesta>();
}