using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Commands.CreateReporteVinculacion
{
    /// <summary>
    /// Validador para el registro
    /// de un reporte de vinculación.
    /// </summary>
    public sealed class CreateReporteVinculacionCommandValidator
        : AbstractValidator<CreateReporteVinculacionCommand>
    {
        public CreateReporteVinculacionCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .GreaterThan(0)
                .WithMessage("El identificador del periodo es obligatorio.");

            RuleFor(x => x.IntTotalConveniosActivos)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total de convenios activos no puede ser negativo.");

            RuleFor(x => x.DecimalPorcentajeLaborando)
                .InclusiveBetween(0, 100)
                .WithMessage("El porcentaje de egresados laborando debe estar entre 0 y 100.");

            When(x => x.BitSeguimientoEgresados, () =>
            {
                RuleFor(x => x.IdMecanismoSeguimiento)
                    .NotNull()
                    .GreaterThan(0)
                    .WithMessage("Debe seleccionar un mecanismo de seguimiento.");
            });

            RuleFor(x => x.SectoresVinculados)
                .NotNull()
                .Must(x => x.Any())
                .WithMessage("Debe seleccionar al menos un sector vinculado.");
        }
    }
}