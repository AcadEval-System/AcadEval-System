using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class PerfilCoordinador
{
    public int Id { get; set; }

    public string UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public int TecnicaturaId { get; set; }
    public virtual Tecnicatura? TecnicaturaCoordinada { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}