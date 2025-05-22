using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcadEvalSys.Domain.Entities;

public class Competencia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<EvaluacionCompetencia> EvaluacionesCompetencias { get; set; } =
        new List<EvaluacionCompetencia>();
}