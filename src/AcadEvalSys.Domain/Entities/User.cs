using Microsoft.AspNetCore.Identity;

namespace AcadEvalSys.Domain.Entities; // Assuming this is your desired namespace

public class User : IdentityUser
{    
    public virtual Student? Student { get; set; }
    public virtual Professor? Professor { get; set; }
    public virtual Coordinator? Coordinator { get; set; }
}