using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class PerfilProfesor
{
    public int Id { get; set; }

    public string UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Asignatura> AsignaturasImpartidas { get; set; } = new List<Asignatura>();
}