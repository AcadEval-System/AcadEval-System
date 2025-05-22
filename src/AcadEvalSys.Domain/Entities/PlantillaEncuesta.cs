using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class PlantillaEncuesta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public string CreadoPorUsuarioId { get; set; }
    public virtual Usuario? CreadoPorUsuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<PreguntaPlantillaEncuesta> PreguntasPlantillaEncuesta { get; set; } =
        new List<PreguntaPlantillaEncuesta>();

    public virtual ICollection<Encuesta> Encuestas { get; set; } = new List<Encuesta>();
}