using FluentValidation;

namespace Seph.Principal.Application.Features.ReportePersonal.Commands
{
    public sealed class ReportePersonalCommandValidator
        : AbstractValidator<CreateReportePersonalCommand>
    {
        public ReportePersonalCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .NotEmpty();

            RuleFor(x => x.IntTotalGeneral)
                .GreaterThan(0)
                .WithMessage("El total general debe ser mayor que cero.");

            RuleFor(x => x.IntTotalDirectivos)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDirectivosHombres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDirectivosMujeres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalAdministrativos)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalAdministrativosHombres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalAdministrativosMujeres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentes)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentesHombres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentesMujeres)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentesTiempoCompleto)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentesAsignatura)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IntTotalDocentesHora)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.IdNivelAcademico)
                .GreaterThan(0);

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty();

            // La distribución por sexo de directivos debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTotalDirectivosHombres +
                    x.IntTotalDirectivosMujeres ==
                    x.IntTotalDirectivos)
                .WithMessage(
                    "La suma de hombres y mujeres directivos debe coincidir con el total de directivos.");

            // La distribución por sexo de administrativos debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTotalAdministrativosHombres +
                    x.IntTotalAdministrativosMujeres ==
                    x.IntTotalAdministrativos)
                .WithMessage(
                    "La suma de hombres y mujeres administrativos debe coincidir con el total de administrativos.");

            // La distribución por sexo de docentes debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTotalDocentesHombres +
                    x.IntTotalDocentesMujeres ==
                    x.IntTotalDocentes)
                .WithMessage(
                    "La suma de hombres y mujeres docentes debe coincidir con el total de docentes.");

            // La distribución por tipo de contratación docente debe coincidir con el total.
            RuleFor(x => x)
                .Must(x =>
                    x.IntTotalDocentesTiempoCompleto +
                    x.IntTotalDocentesAsignatura +
                    x.IntTotalDocentesHora ==
                    x.IntTotalDocentes)
                .WithMessage(
                    "La suma de docentes de tiempo completo, asignatura y por hora debe coincidir con el total de docentes.");
        }
    }
}