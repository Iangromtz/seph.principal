using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.CatPeriodos.Commands.UpdateCatPeriodo
{
    /// <summary>
    /// Valida los datos necesarios para actualizar un periodo.
    /// </summary>
    public sealed class UpdateCatPeriodoCommandValidator
        : AbstractValidator<UpdateCatPeriodoCommand>
    {
        public UpdateCatPeriodoCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El identificador del periodo no es válido.");

            RuleFor(x => x.IntAnio)
                .GreaterThan(0)
                .WithMessage("El año del periodo es obligatorio.");

            RuleFor(x => x.IntNumeroPeriodo)
                .GreaterThan(0)
                .WithMessage("El número de periodo debe ser mayor a cero.");

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
                .Equal(x => x.DateFechaInicio.Year)
                .WithMessage("El año del periodo debe coincidir con el año de la fecha de inicio.");
        }
    }
}
