using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class PreguntaPlantillaEncuesta
{
    public int Id { get; set; }

    public int PlantillaId { get; set; }
    public virtual PlantillaEncuesta? PlantillaEncuesta { get; set; }

    public string TextoPregunta { get; set; } = string.Empty;
    public string TipoPregunta { get; set; } = string.Empty;
    public string? Opciones { get; set; }
    public int OrdenEnPlantilla { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}