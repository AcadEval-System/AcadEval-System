using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AcadEvalSys.Application.Career.Commands.CreateCareer
{
    public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
    {
        public CreateCareerCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.");
        }
    }
}
