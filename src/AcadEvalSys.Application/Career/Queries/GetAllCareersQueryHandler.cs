using AcadEvalSys.Application.Career.Dtos;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Career.Queries;

public class GetAllCareersQueryHandler(ILogger<GetAllCareersQueryHandler> logger, ICareerRepository careerRepository, IMapper mapper) : IRequestHandler<GetAllCareersQuery, IEnumerable<CareerDto>>
{
    public async Task<IEnumerable<CareerDto>> Handle(GetAllCareersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all careers"); 
        var careers = await careerRepository.GetAllCareersAsync();
        var careerDtos = mapper.Map<IEnumerable<CareerDto>>(careers);
        logger.LogInformation("Successfully retrieved {Count} careers", careerDtos.Count());
        return careerDtos;
    }
}