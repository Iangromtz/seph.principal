using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Queries.GetInstituciones
{
    public sealed record GetInstitucionesQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<InstitucionDto>>>;
}
