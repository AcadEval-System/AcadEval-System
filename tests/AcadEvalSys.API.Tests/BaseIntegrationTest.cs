using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Constants.Constants;
using Microsoft.AspNetCore.Authorization.Policy;
using Xunit;

namespace AcadEvalSys.API.Tests;

public abstract class BaseIntegrationTest : IAsyncLifetime, IDisposable
{
    protected WebApplicationFactory<Program> Factory { get; private set; } = null!;
    protected JsonSerializerOptions JsonOptions { get; private set; } = null!;
    protected HttpClient Client { get; private set; } = null!;
    
    // Cada test tendrá sus propios mocks completamente aislados
    protected Mock<IUserContext> UserContextMock = new();

    public virtual async Task InitializeAsync()
    {
        // Reset todos los mocks antes de cada test para asegurar aislamiento
        ResetMocks();
        
        // Configurar usuario por defecto
        SetupDefaultUser();
        
        // Crear factory con configuración específica
        Factory = CreateFactory();
        
        // Crear cliente HTTP
        Client = Factory.CreateClient();
        
        // Configurar opciones JSON
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        
        await Task.CompletedTask;
    }

    public virtual async Task DisposeAsync()
    {
        Client?.Dispose();
        Factory?.Dispose();
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        Client?.Dispose();
        Factory?.Dispose();
        GC.SuppressFinalize(this);
    }

    protected virtual void ResetMocks()
    {
        UserContextMock.Reset();
        ResetSpecificMocks();
    }

    // Método virtual para que las clases derivadas puedan resetear sus propios mocks
    protected virtual void ResetSpecificMocks()
    {
    }

    protected virtual void SetupDefaultUser()
    {
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin]);
        UserContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);
    }

    protected virtual void SetupCustomUser(string id, string email, params string[] roles)
    {
        var currentUser = new CurrentUser(id, email, roles);
        UserContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);
    }

    protected abstract WebApplicationFactory<Program> CreateFactory();

    // Método helper para configurar servicios de test específicos
    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
        services.Replace(ServiceDescriptor.Scoped(typeof(IUserContext), _ => UserContextMock.Object));
    }
} 