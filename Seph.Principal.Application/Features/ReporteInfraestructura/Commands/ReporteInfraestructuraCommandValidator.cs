using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Commands
{
    /// <summary>
    /// Valida la información necesaria para registrar
    /// un reporte de infraestructura.
    /// </summary>
    public sealed class ReporteInfraestructuraCommandValidator
        : AbstractValidator<CreateReporteInfraestructuraCommand>
    {
        public ReporteInfraestructuraCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .NotEmpty();

            RuleFor(x => x.IntTotalAulas)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "El total de aulas no puede ser menor que cero.");

            RuleFor(x => x.IntTotalLaboratorios)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "El total de laboratorios no puede ser menor que cero.");

            RuleFor(x => x.IntTotalTalleres)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "El total de talleres no puede ser menor que cero.");

            RuleFor(x => x.IntTotalBibliotecas)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "El total de bibliotecas no puede ser menor que cero.");

            RuleFor(x => x.IntTotalComputo)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "El total de equipos de cómputo no puede ser menor que cero.");

            RuleFor(x => x.IdInternet)
                .GreaterThan(0)
                .WithMessage(
                    "Debe seleccionar una opción de acceso a internet.");

            RuleFor(x => x.IdDiscapacitado)
                .GreaterThan(0)
                .WithMessage(
                    "Debe seleccionar una opción de infraestructura para personas con discapacidad.");

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty();

            // Si la institución cuenta con biblioteca,
            // debe registrarse al menos una.
            RuleFor(x => x)
                .Must(x =>
                    !x.BitBiblioteca ||
                    x.IntTotalBibliotecas > 0)
                .WithMessage(
                    "Debe registrar al menos una biblioteca cuando indique que la institución cuenta con biblioteca.");

            // Si la institución no cuenta con biblioteca,
            // el total debe permanecer en cero.
            RuleFor(x => x)
                .Must(x =>
                    x.BitBiblioteca ||
                    x.IntTotalBibliotecas == 0)
                .WithMessage(
                    "El total de bibliotecas debe ser cero cuando la institución no cuenta con biblioteca.");
        }
    }
}