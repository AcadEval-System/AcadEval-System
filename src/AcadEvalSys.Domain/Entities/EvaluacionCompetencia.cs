using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class EvaluacionCompetencia
{
    public int Id { get; set; }

    public int EvaluacionId { get; set; }
    public virtual Evaluacion? Evaluacion { get; set; }

    public int CompetenciaId { get; set; }
    public virtual Competencia? Competencia { get; set; }

    public int? Calificacion { get; set; } // Renamed from Rating (e.g., a numerical score)
    public string? Comentarios { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}