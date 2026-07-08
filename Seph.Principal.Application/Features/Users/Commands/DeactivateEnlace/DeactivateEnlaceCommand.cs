using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Users.Commands.DeactivateEnlace
{
    public sealed record DeactivateEnlaceCommand(Guid Id) : IRequest<ResponseWrapper<string>>;
}
