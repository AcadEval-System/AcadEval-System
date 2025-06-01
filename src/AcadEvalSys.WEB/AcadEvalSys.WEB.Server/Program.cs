using AcadEvalSys.Application.Extensions;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Infrastructure.Extensions;
using AcadEvalSys.Infrastructure.Seeders;
using AcadEvalSys.WEB.Server.Extensions;
using AcadEvalSys.WEB.Server.Middlewares;
using Microsoft.OpenApi.Models;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Presentation layer services, including Serilog, are added here
    builder.AddPresentation(); 
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    var app = builder.Build();
    

    
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ErrorHandlingMiddleware>();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

    await seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcadEval API v1"));
    }

    app.UseHttpsRedirection();
    app.UseDefaultFiles();
    app.UseStaticFiles();
   
    app.UsePathBase("/api");
    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("identity")
        .MapIdentityApi<User>()
        .WithTags("Identity"); // Agrupa estos endpoints bajo la etiqueta 'Auth' en Swagger

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    var serverAddress = app.Urls.FirstOrDefault() ?? "https://localhost:7004";
    var machineName = Environment.MachineName;
    var environment = app.Environment.EnvironmentName;
    Log.Information("Server starting... Target URL: {ServerUrl} on machine {MachineName} ({Environment})",
        serverAddress, machineName, environment);

    app.Run();
    
}
catch (Exception ex)
{
    Log.Fatal(ex.Message, "Error in app startup");
}
finally
{
    Log.CloseAndFlush();
}