using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AcadEvalSys.Application.Career.Commands.UpdateCareer
{
    public class UpdateCareerCommandValidator : AbstractValidator<UpdateCareerCommand>
    {
        public UpdateCareerCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.");

        }
    }
}
