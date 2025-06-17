using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Career.Commands.DeleteCareer
{
    public class DeleteRestaurantCommandHandler(ILogger<CreateCareerCommandHandler> logger, ICareerRepository careerRepository) : IRequestHandler<DeleteCareerCommand>
    {
        public async Task Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting career with id {TechnicalCareerId}", request.Id);

            var career = await careerRepository.GetCareerByIdAsync(request.Id);

            if (career == null)
            {
                throw new NotFoundException(nameof(TechnicalCareer), request.Id.ToString());
            }
            career.IsActive = false;
            await careerRepository.Delete(career);
        }

    }
}
