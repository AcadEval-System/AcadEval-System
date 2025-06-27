namespace AcadEvalSys.Application.Users.Services;

/// <summary>
/// Servicio para obtener información de la sesión actual
/// </summary>
public interface ISessionService
{
    Task<DateTimeOffset?> GetSessionExpiration();
    int? GetMinutesRemaining();
} 