using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Professor.Commands.CreateProfessor;

public class AddProfesorCommandHandler(ILogger<AddProfesorCommandHandler> logger, IMapper mapper, UserManager<User> userManager, IProfessorRepository professorRepository) : IRequestHandler<AddProfessorCommand, string>
{
    public async Task<string> Handle(AddProfessorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new Professor {Name}", request.Name);
        var user = mapper.Map<User>(request);
        await userManager.CreateAsync(user, request.Password);
        await userManager.AddToRoleAsync(user, UserRoles.Professor);

        var professor = new Domain.Entities.Professor();
        professor.UserId = user.Id;

        await professorRepository.
    }
}