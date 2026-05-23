using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Auth.Commands.RevokeSession
{
    public sealed record RevokeSessionCommand(string RefreshToken):IRequest<ResponseWrapper<bool>>;
   
}
