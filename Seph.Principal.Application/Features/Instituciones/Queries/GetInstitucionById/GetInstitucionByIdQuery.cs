using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Queries.GetInstitucionById
{
    public sealed record GetInstitucionByIdQuery(long Id)
        : IRequest<ResponseWrapper<InstitucionDto>>;
}
