using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Application.Career.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Career.Queries.GetCareerById
{
    public class GetCareerByIdQueryHandler(ILogger<GetCareerByIdQueryHandler> logger, IMapper mapper, ICareerRepository careerRepository) : IRequestHandler<GetCareerByIdQuery, CareerDto>
    {
        public async Task<CareerDto> Handle(GetCareerByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting career {TechnicalCareerId}", request.Id);
            var career = await careerRepository.GetCareerByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(TechnicalCareer), request.Id.ToString());

            var careerDto = mapper.Map<CareerDto>(career);

            return careerDto;
        }
    }
}
