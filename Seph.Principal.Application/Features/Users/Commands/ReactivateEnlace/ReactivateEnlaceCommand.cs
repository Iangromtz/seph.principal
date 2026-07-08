using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Users.Commands.ReactivateEnlace
{
    public sealed record ReactivateEnlaceCommand(Guid Id) : IRequest<ResponseWrapper<string>>;
}
