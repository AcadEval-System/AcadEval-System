using AcadEvalSys.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Students.Commands.RemoveStudent;

public class RemoveStudentCommandHandler(Logger<RemoveStudentCommandHandler> logger, Mapper mapper, UserStore<User> userStore) : IRequestHandler<RemoveStudentCommand>
{
    public Task Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}