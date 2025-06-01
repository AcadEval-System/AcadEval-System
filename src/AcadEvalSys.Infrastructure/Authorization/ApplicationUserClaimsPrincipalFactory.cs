using System.Security.Claims;
using AcadEvalSys.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AcadEvalSys.Infrastructure.Authorization;

public class ApplicationUserClaimsPrincipalFactory(
UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, optionsAccessor)
{
public override async Task<ClaimsPrincipal> CreateAsync(User user)
{
    var id = await GenerateClaimsAsync(user);
  

    return new ClaimsPrincipal(id);
    }
}