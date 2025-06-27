using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadEvalSys.Application.Users.Dtos
{

public record SessionStatusDto
{
    public bool IsAuthenticated { get; init; }
    public CurrentUserDto? User { get; init; }
    public DateTimeOffset? ExpiresAt { get; init; }
    public int? MinutesRemaining { get; init; }
}
}