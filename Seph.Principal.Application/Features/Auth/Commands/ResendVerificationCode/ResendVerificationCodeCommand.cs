using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Auth.Commands.ResendVerificationCode
{
    public sealed record ResendVerificationCodeCommand(string Email)
        : IRequest<ResponseWrapper<string>>;
}