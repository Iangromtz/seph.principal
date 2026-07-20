using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.CreateCatPeriodo
{

    /// <summary>
    /// Valida los datos necesarios para registrar un periodo.
    /// </summary>
    public sealed class CreateCatPeriodoCommandValidator
        : AbstractValidator<CreateCatPeriodoCommand>
    {
        public CreateCatPeriodoCommandValidator()
        {
            RuleFor(x => x.IntAnio)
                .GreaterThan(0)
                .WithMessage("El año del periodo es obligatorio.");

            RuleFor(x => x.IntNumeroPeriodo)
                .GreaterThan(0)
                .WithMessage("El número de periodo debe ser mayor que cero.");

            RuleFor(x => x.DateFechaInicio)
                .NotEmpty()
                .WithMessage("La fecha de inicio es obligatoria.");

            RuleFor(x => x.DateFechaFin)
                .NotEmpty()
                .WithMessage("La fecha de fin es obligatoria.");

            RuleFor(x => x.DateFechaFin)
                .GreaterThanOrEqualTo(x => x.DateFechaInicio)
                .WithMessage("La fecha de fin no puede ser menor que la fecha de inicio.");

            RuleFor(x => x.IntAnio)
                .Must((command, intAnio) =>
                    intAnio == command.DateFechaInicio.Year)
                .WithMessage("El año debe coincidir con el año de la fecha de inicio.");
        }
    }
}
