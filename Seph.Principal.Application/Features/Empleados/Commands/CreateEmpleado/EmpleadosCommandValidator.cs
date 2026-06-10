using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado
{
    public sealed class EmpleadosCommandValidator
    : AbstractValidator<CreateEmpleadoCommand>
    {
        public EmpleadosCommandValidator() 
        {
            RuleFor(x => x.StrNombre)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.StrApellidoPat)
               .NotEmpty()
               .MaximumLength(250);

            RuleFor(x => x.StrApellidoMat)
               .NotEmpty()
               .MaximumLength(250);

            RuleFor(x => x.StrCurp)
               .NotEmpty()
               .MaximumLength(18);

            RuleFor(x => x.IdSexo)
                .NotEmpty();

            RuleFor(x => x.DateTimeFechaRegistro)
                .NotEmpty();

            RuleFor(x => x.IdUsuarioRegistro)
               .NotEmpty();

            RuleFor(x => x.BitActivo)
               .NotEmpty();

            RuleFor(x => x.DateTimeFechaBaja)
            .Must(f => f != default(DateTime))
            .WithMessage("La fecha de baja no es válida.")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("La fecha de baja no puede ser anterior a hoy.");
        }
    }
}
