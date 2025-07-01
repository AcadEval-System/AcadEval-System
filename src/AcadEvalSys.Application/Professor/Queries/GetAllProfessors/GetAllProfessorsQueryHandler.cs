using AcadEvalSys.Application.Professor.Commands.UpdateProfessor;
using AcadEvalSys.Application.Professor.Dtos;
using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Professor.Queries.GetAllProfessors;

public class GetAllProfessorsQueryHandler(ILogger<UpdateProfessorCommandHandler> logger, IMapper mapper, UserManager<User> userManager) : IRequestHandler<GetAllProfessorsQuery, IEnumerable<ProfessorDto>>
{
    public Task<IEnumerable<ProfessorDto>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all professors");

        var users = userManager.GetUsersInRoleAsync(UserRoles.Professor).Result;

        if (users == null || !users.Any())
        {
            logger.LogInformation("No professors found");
            return Task.FromResult<IEnumerable<ProfessorDto>>(new List<ProfessorDto>());
        }

        var professorsDto = mapper.Map<IEnumerable<ProfessorDto>>(users);

        logger.LogInformation("Successfully retrieved {Count} professors", professorsDto.Count());

        return Task.FromResult(professorsDto);
    }
}