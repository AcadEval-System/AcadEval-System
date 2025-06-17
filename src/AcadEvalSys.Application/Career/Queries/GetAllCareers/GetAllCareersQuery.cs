using AcadEvalSys.Application.Career.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Career.Queries.GetAllCareers;

public class GetAllCareersQuery : IRequest<IEnumerable<CareerDto>>
{
    
}