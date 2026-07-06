using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Commands.ReactivateInstitucion
{
    public sealed record ReactivateInstitucionCommand(long Id) : IRequest<ResponseWrapper<string>>;
}
