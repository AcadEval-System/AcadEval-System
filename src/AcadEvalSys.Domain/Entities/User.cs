using Microsoft.AspNetCore.Identity;

namespace AcadEvalSys.Domain.Entities; // Assuming this is your desired namespace

public class User : IdentityUser
{    
    public virtual Student? Student { get; private set; }
    public virtual Professor? Professor { get; private set; }
    public virtual Coordinator? Coordinator { get; private set; }
    
}