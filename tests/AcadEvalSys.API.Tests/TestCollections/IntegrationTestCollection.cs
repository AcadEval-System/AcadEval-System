using Xunit;

namespace AcadEvalSys.API.Tests.TestCollections;

[CollectionDefinition("Integration Tests")]
public class IntegrationTestCollection
{
    // Esta clase existe solo para definir la colección
    // Todos los tests que usen [Collection("Integration Tests")] se ejecutarán secuencialmente
    // No necesita fixture porque cada test maneja su propio ciclo de vida
} 