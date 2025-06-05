using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Application.Career.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Career.Queries.GetCareerById
{
    public class GetCareerByIdQuery(Guid id) : IRequest<CareerDto>
    {
        public Guid Id { get; } = id;
    }
}
