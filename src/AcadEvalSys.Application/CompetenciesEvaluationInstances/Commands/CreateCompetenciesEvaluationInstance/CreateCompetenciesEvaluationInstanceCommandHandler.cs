using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.CreateCompetenciesEvaluationInstance;

public class CreateCompetenciesEvaluationInstanceCommandHandler(
    ILogger<CreateCompetenciesEvaluationInstanceCommandHandler> logger,
    ICompetenciesEvaluationInstanceRepository competenciesEvaluationInstanceRepository,
    IProfessorCompetencyAssignmentRepository professorCompetencyAssignmentRepository,
    ICompetencyRepository competencyRepository,
    ISubjectRepository subjectRepository,
    IMapper mapper) : IRequestHandler<CreateCompetenciesEvaluationInstanceCommand, Guid>
{
    public async Task<Guid> Handle(CreateCompetenciesEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating CompetenciesEvaluationInstance with title: {Title}", request.Title);

        // Validar que todas las competencias y materias existen
        await ValidateAssignmentsAsync(request.CompetencyAssignments);

        // Crear la instancia de evaluación
        var competenciesEvaluationInstance = mapper.Map<CompetenciesEvaluationInstance>(request);
        var competenciesEvaluationInstanceId = await competenciesEvaluationInstanceRepository.CreateCompetenciesEvaluationInstanceAsync(competenciesEvaluationInstance);

        logger.LogInformation("CompetenciesEvaluationInstance created with ID: {Id}", competenciesEvaluationInstanceId);

        // Crear las asignaciones de competencias a profesores
        var professorAssignments = new List<ProfessorCompetencyAssignment>();

        foreach (var assignment in request.CompetencyAssignments)
        {
            var professorAssignment = mapper.Map<ProfessorCompetencyAssignment>(assignment);
            professorAssignment.CompetenciesEvaluationInstanceId = competenciesEvaluationInstanceId;

            professorAssignments.Add(professorAssignment);
        }

        if (professorAssignments.Any())
        {
            await professorCompetencyAssignmentRepository.CreateMultipleAsync(professorAssignments);
            logger.LogInformation("Created {Count} professor competency assignments", professorAssignments.Count);
        }

        return competenciesEvaluationInstanceId;
    }

    private async Task ValidateAssignmentsAsync(CreateCompetencyAssignmentDto[] assignments)
    {
        var competencyIds = assignments.Select(a => a.CompetencyId).Distinct().ToList();
        var subjectIds = assignments.Select(a => a.SubjectId).Distinct().ToList();

        // Validar competencias
        foreach (var competencyId in competencyIds)
        {
            var competencyExists = await competencyRepository.ExistsByIdAsync(competencyId);
            if (!competencyExists)
            {
                throw new NotFoundException(nameof(Competency), competencyId.ToString());
            }
        }

        // Validar materias
        foreach (var subjectId in subjectIds)
        {
            var subjectExists = await subjectRepository.ExistsByIdAsync(subjectId);
            if (!subjectExists)
            {
                throw new NotFoundException(nameof(Subject), subjectId.ToString());
            }
        }
    }
}