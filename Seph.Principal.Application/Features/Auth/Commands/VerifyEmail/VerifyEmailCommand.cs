using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Auth.Commands.VerifyEmail
{
    public sealed record VerifyEmailCommand(string Email, string Code)
        : IRequest<ResponseWrapper<string>>;
}