using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteMatricula.Commands
{
    public sealed class ReporteMatriculaCommandValidator
         : AbstractValidator<CreateReporteMatriculaCommand>
    {
        public ReporteMatriculaCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
               .NotEmpty();

            RuleFor(x => x.IntTotal)
                .GreaterThan(0)
                .WithMessage("La matrícula total debe ser mayor que cero.");

            RuleFor(x => x.IntTotalHombres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalMujeres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTsu)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntLicenciatura)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntPostgrado)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.DecimalTazaDesercion)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.DecimalTazaReprobacion)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.DecimalTazaEficienciaTerminal)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty();

            // La distribución por sexo debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTotalHombres +
                    x.IntTotalMujeres ==
                    x.IntTotal)
                .WithMessage(
                    "La suma de hombres y mujeres debe coincidir con la matrícula total.");

            // La distribución por nivel debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTsu +
                    x.IntLicenciatura +
                    x.IntPostgrado ==
                    x.IntTotal)
                .WithMessage(
                    "La suma de TSU, licenciatura y posgrado debe coincidir con la matrícula total.");
        
    }
    }
}
