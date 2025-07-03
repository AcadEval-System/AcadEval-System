using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Subjects.Commands.EnrollStudent;

public class EnrollStudentInSubjectCommandHandler(ILogger<EnrollStudentInSubjectCommandHandler> logger, ISubjectRepository subjectRepository, IStudentRepository studentRepository) : IRequestHandler<EnrollStudentInSubjectCommand, bool>
{
    public async Task<bool> Handle(EnrollStudentInSubjectCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Enrolling student {StudentId} in subject {SubjectId}", request.StudentId, request.SubjectId);

        // validar que existe la materia
        var subject = await subjectRepository.GetSubjectByIdAsync(request.SubjectId);
        if (subject == null)
        {
            logger.LogWarning("Subject with ID {SubjectId} not found", request.SubjectId);
            throw new NotFoundException(nameof(Subject), request.SubjectId.ToString());
        }

        // validar que el estudiante existe y pertenece a la carrera de la materia
        var studentExists = await subjectRepository.StudentExistsAsync(request.StudentId, subject.TechnicalCareerId!.Value);
        if (!studentExists)
        {
            logger.LogWarning("Student with ID {StudentId} not found or does not belong to career {TechnicalCareerId} for subject {SubjectId}",
                request.StudentId, subject.TechnicalCareerId, request.SubjectId);
            throw new NotFoundException(nameof(Student), request.StudentId);
        }

        // validar que el estudiante no esté ya inscrito
        var isAlreadyEnrolled = await subjectRepository.IsStudentEnrolledInSubjectAsync(request.StudentId, request.SubjectId);
        if (isAlreadyEnrolled)
        {
            logger.LogWarning("Student {StudentId} is already enrolled in subject {SubjectId}", request.StudentId, request.SubjectId);
            return false;
        }

        try
        {
            await subjectRepository.EnrollStudentInSubjectAsync(request.StudentId, request.SubjectId);
            logger.LogInformation("Student {StudentId} enrolled in subject {SubjectId} successfully", request.StudentId, request.SubjectId);
            return true;
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Error enrolling student {StudentId} in subject {SubjectId}", request.StudentId, request.SubjectId);
            return false;
        }
    }
}