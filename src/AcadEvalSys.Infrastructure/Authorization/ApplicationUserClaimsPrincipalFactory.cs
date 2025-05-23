using System.Security.Claims;
using AcadEvalSys.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AcadEvalSys.Infrastructure.Authorization;

public class ApplicationUserClaimsPrincipalFactory(
UserManager<Usuario> userManager,
    RoleManager<IdentityRole> roleManager,
IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<Usuario, IdentityRole>(userManager, roleManager, optionsAccessor)
{
public override async Task<ClaimsPrincipal> CreateAsync(Usuario user)
{
    var id = await GenerateClaimsAsync(user);
  

    return new ClaimsPrincipal(id);
    }
}