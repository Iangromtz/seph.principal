using FluentValidation;

namespace Seph.Principal.Application.Features.Auth.Commands.VerifyEmail
{
    public sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Code).NotEmpty().Length(5).Matches("^[A-Z0-9]+$");
        }
    }
}