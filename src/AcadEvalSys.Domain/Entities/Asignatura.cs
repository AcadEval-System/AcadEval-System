using System;
using System.Collections.Generic;

namespace AcadEvalSys.Domain.Entities;

public class Asignatura
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int AnioTecnicatura { get; set; }
    public int TecnicaturaId { get; set; }
    public virtual Tecnicatura? Tecnicatura { get; set; }

    public int? PerfilProfesorId { get; set; }
    public virtual PerfilProfesor? PerfilProfesor { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}