using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Commands.DeactivateInstitucion
{
    public sealed record DeactivateInstitucionCommand(long Id)
        : IRequest<ResponseWrapper<string>>;
}
