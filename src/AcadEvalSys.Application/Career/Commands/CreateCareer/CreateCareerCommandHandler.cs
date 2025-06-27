using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Career.Commands.CreateCareer
{
    public class CreateCareerCommandHandler(ILogger<CreateCareerCommandHandler> logger, IMapper mapper, ICareerRepository careerRepository ) : IRequestHandler<CreateCareerCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new career {@TechnicalCareer}", request);

            var career = mapper.Map<TechnicalCareer>(request);

            var id = await careerRepository.Create(career);
            return id;
        }
    }
    
}
