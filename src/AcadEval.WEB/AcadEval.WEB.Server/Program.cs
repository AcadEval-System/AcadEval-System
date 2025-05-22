using AcadEval.WEB.Extensions;
using AcadEval.WEB.Middlewares;
using AcadEvalSys.Application.Extensions;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    var app = builder.Build();
    
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcadEval API v1"));
    }

    app.UseHttpsRedirection();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseRouting();
    app.UsePathBase("/api");
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("identity")
        .MapIdentityApi<Usuario>()
        .WithTags("Identity"); // Agrupa estos endpoints bajo la etiqueta 'Auth' en Swagger

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Error in app startup");
}
finally
{
    Log.CloseAndFlush();
}