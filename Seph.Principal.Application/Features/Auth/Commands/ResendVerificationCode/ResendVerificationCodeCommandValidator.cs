using FluentValidation;

namespace Seph.Principal.Application.Features.Auth.Commands.ResendVerificationCode
{
    public sealed class ResendVerificationCodeCommandValidator : AbstractValidator<ResendVerificationCodeCommand>
    {
        public ResendVerificationCodeCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        }
    }
}