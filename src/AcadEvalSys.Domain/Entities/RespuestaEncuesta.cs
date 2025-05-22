using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class RespuestaEncuesta
{
    public int Id { get; set; }

    public int EncuestadoId { get; set; }
    public virtual Encuestado? Encuestado { get; set; }

    public int PreguntaEncuestaId { get; set; }
    public virtual PreguntaEncuesta? PreguntaEncuesta { get; set; }

    public string? ValorRespuesta { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}