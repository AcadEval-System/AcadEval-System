using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Enums;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Students.Commands.AddStudent;

public class AddStudentCommandHandler(
    ILogger<AddStudentCommandHandler> logger,
    UserManager<User> userManager,
    IStudentRepository studentRepository,
    IMapper mapper
    ) : IRequestHandler<AddStudentCommand, string>
{
    public async Task<string> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding student {@Student}", request);
        
        var user = mapper.Map<User>(request);
        
        var result = await userManager.CreateAsync(user, request.Password);

        await userManager.AddToRoleAsync(user, UserRoles.Student);

        var student = new Student();
        student.UserId = user.Id; 
        student.TechnicalCareerId = request.CarreraId;

        await studentRepository.EnrollStudentInCareerAsync(student);
        

        logger.LogInformation("Student created successfully with ID: {StudentId}", user.Id);
        return user.Id;
    }
}



