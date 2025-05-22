using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcadEvalSys.Domain.Entities;

public class Tecnicatura
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<PerfilAlumno> PerfilesAlumnos { get; set; } = new List<PerfilAlumno>();
    public int? PerfilCoordinadorId { get; set; }
    public virtual PerfilCoordinador? PerfilCoordinador { get; set; }
    public virtual ICollection<Asignatura> Materias { get; set; } = new List<Asignatura>();
}