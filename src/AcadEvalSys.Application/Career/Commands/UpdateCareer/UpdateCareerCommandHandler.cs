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
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Career.Commands.UpdateCareer
{
    public class UpdateCareerCommandHandler(ILogger<UpdateCareerCommandHandler> logger, IMapper mapper, ICareerRepository careerRepository) : IRequestHandler<UpdateCareerCommand>
    {
        public async Task Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

            var restaurant = await careerRepository.GetCareerByIdAsync(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(TechnicalCareer), request.Id.ToString());
            }

            mapper.Map(request, restaurant);

            await careerRepository.Update();
        }
    }
}
