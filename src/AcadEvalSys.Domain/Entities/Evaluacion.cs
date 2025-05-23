using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class Evaluacion
{
    public int Id { get; set; }

    public string EvaluadorUsuarioId { get; set; }
    public virtual Usuario? EvaluadorUsuario { get; set; }

    public string EvaluadoUsuarioId { get; set; }
    public virtual Usuario? EvaluadoUsuario { get; set; }

    public DateTime FechaEvaluacion { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } = string.Empty;
    public string? ComentariosGenerales { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<EvaluacionCompetencia> EvaluacionesCompetencias { get; set; } =
        new List<EvaluacionCompetencia>();
}