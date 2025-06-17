using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AcadEvalSys.Application.Career.Commands.DeleteCareer
{
    public class DeleteCareerCommand(Guid id) : IRequest
    {
        public Guid Id { get; } = id;
    }
}
