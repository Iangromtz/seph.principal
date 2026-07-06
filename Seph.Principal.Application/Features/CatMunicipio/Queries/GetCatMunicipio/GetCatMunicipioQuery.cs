using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMunicipio.DTOs;

namespace Seph.Principal.Application.Features.CatMunicipio.Queries.GetCatMunicipio
{
    public sealed record GetCatMunicipioQuery()
        : IRequest<ResponseWrapper<IReadOnlyList<CatMunicipioDto>>>;
}
