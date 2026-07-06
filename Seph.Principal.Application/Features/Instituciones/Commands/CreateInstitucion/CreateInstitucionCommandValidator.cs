using FluentValidation;

namespace Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion
{
    public sealed class CreateInstitucionCommandValidator : AbstractValidator<CreateInstitucionCommand>
    {
        public CreateInstitucionCommandValidator()
        {
            RuleFor(x => x.StrNombre)
                .NotEmpty().WithMessage("El nombre de la institución es obligatorio.")
                .MaximumLength(250);

            RuleFor(x => x.IdMunicipio)
                .GreaterThan(0).WithMessage("Debe indicar el municipio de la institución.");

            RuleFor(x => x.StrCorreoInstitucional)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.StrCorreoInstitucional))
                .WithMessage("El correo institucional no es válido.")
                .MaximumLength(250);

            RuleFor(x => x.StrSiglas)
                .MaximumLength(50)
                .NotEmpty().WithMessage("Las siglas de la institución es obligatoria.");

            RuleFor(x => x.StrCct)
                .MaximumLength(30)
                .NotEmpty().WithMessage("El CCT de la institución es obligatorio.");

            RuleFor(x => x.StrDireccion)
                .MaximumLength(500)
                .NotEmpty().WithMessage("La dirección de la institución es obligatorio");
            RuleFor(x => x.StrDecretoCreacion).MaximumLength(500);
            RuleFor(x => x.StrSitioWeb).MaximumLength(250);
            RuleFor(x => x.StrTelefonoInstitucional).MaximumLength(20);
        }
    }
}
