using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class EstadoWizard
{
    public int Id { get; set; }

    public string UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public string TipoWizard { get; set; } = string.Empty;
    public int PasoActual { get; set; }
    public string? DatosEstado { get; set; }
    public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;
}