using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.CreateCompetencyEvaluationInstance;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.CreateCompetenciesEvaluationInstance;

public class CreateCompetencyEvaluationInstanceCommandHandler(
    ILogger<CreateCompetencyEvaluationInstanceCommandHandler> logger,
    ICompetencyEvaluationInstanceRepository competencyEvaluationInstanceRepository,
    IProfessorCompetencyAssignmentRepository professorCompetencyAssignmentRepository,
    ICompetencyRepository competencyRepository,
    ISubjectRepository subjectRepository,
    IMapper mapper) : IRequestHandler<CreateCompetencyEvaluationInstanceCommand, Guid>
{
    public async Task<Guid> Handle(CreateCompetencyEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating CompetencyEvaluationInstance with title: {Title}", request.Title);

        // Validar que todas las competencias y materias existen
        await ValidateAssignmentsAsync(request.CompetencyAssignments);

        // Crear la instancia de evaluación
        var competencyEvaluationInstance = mapper.Map<CompetencyEvaluationInstance>(request);
        var competencyEvaluationInstanceId = await competencyEvaluationInstanceRepository.CreateCompetencyEvaluationInstanceAsync(competencyEvaluationInstance);

        logger.LogInformation("CompetencyEvaluationInstance created with ID: {Id}", competencyEvaluationInstanceId);

        // Crear las asignaciones de competencias a profesores
        var professorAssignments = new List<ProfessorCompetencyAssignment>();

        foreach (var assignment in request.CompetencyAssignments)
        {
            var professorAssignment = mapper.Map<ProfessorCompetencyAssignment>(assignment);
            professorAssignment.CompetencyEvaluationInstanceId = competencyEvaluationInstanceId;

            professorAssignments.Add(professorAssignment);
        }

        if (professorAssignments.Any())
        {
            await professorCompetencyAssignmentRepository.CreateMultipleAsync(professorAssignments);
            logger.LogInformation("Created {Count} professor competency assignments", professorAssignments.Count);
        }

        return competencyEvaluationInstanceId;
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