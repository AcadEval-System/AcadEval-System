using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class Encuesta
{
    public int Id { get; set; }

    public int? PlantillaId { get; set; }
    public virtual PlantillaEncuesta? PlantillaEncuesta { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;

    public string CreadoPorUsuarioId { get; set; }
    public virtual Usuario? CreadoPorUsuario { get; set; }

    public string DescripcionAudienciaObjetivo { get; set; } = string.Empty;
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<PreguntaEncuesta> PreguntasEncuesta { get; set; } = new List<PreguntaEncuesta>();
    public virtual ICollection<Encuestado> Encuestados { get; set; } = new List<Encuestado>();
}