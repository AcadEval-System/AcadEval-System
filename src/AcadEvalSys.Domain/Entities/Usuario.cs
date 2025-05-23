using Microsoft.AspNetCore.Identity;

namespace AcadEvalSys.Domain.Entities; // Assuming this is your desired namespace

public class Usuario : IdentityUser
{
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    // Navigation properties to profile data
    public virtual PerfilAlumno? PerfilAlumno { get; set; }
    public virtual PerfilProfesor? PerfilProfesor { get; set; }
    public virtual PerfilCoordinador? PerfilCoordinador { get; set; }
    // No profile for Administrador, or it's handled differently
}