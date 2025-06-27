using System.Threading.Tasks;
using AcadEvalSys.Application.Users.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Infrastructure.Services;


public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SessionService> _logger;

    public SessionService(IHttpContextAccessor httpContextAccessor, ILogger<SessionService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<DateTimeOffset?> GetSessionExpiration()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            // Obtener el ticket de autenticación de las cookies
            var authenticateResult = await httpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
            
            if (!authenticateResult.Succeeded || authenticateResult.Properties == null)
            {
                _logger.LogWarning("No se pudo obtener el ticket de autenticación");
                return null;
            }

            // Obtener la expiración del ticket
            var expiresUtc = authenticateResult.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                _logger.LogDebug("Sesión expira en: {ExpiresAt}", expiresUtc.Value);
                return expiresUtc.Value;
            }

            // Si no hay expiración explícita, calcular basado en la configuración
            var issuedUtc = authenticateResult.Properties.IssuedUtc;
            if (issuedUtc.HasValue)
            {
                // Usar la configuración por defecto (120 días como tienes configurado)
                var calculatedExpiration = issuedUtc.Value.AddDays(120);
                _logger.LogDebug("Calculando expiración basada en emisión: {ExpiresAt}", calculatedExpiration);
                return calculatedExpiration;
            }

            _logger.LogWarning("No se pudo determinar la expiración de la sesión");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la expiración de la sesión");
            return null;
        }
    }

    public async Task<int?> GetMinutesRemaining()
    {
        var expiration = await GetSessionExpiration();
        if (!expiration.HasValue)
        {
            return null;
        }

        var remaining = expiration.Value.Subtract(DateTimeOffset.UtcNow).TotalMinutes;
        return remaining > 0 ? (int)Math.Ceiling(remaining) : 0;
    }
} 