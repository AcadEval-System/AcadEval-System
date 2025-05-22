using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Infrastructure.Authorization;
using AcadEvalSys.Infrastructure.Persistence;

namespace AcadEvalSys.Infrastructure.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("AcadEvalDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'AcadEval' not found.");
            }

            options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging();
        });
        services.AddIdentityApiEndpoints<Usuario>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}