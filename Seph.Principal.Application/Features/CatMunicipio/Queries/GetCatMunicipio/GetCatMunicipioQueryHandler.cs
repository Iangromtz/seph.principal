using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMunicipio.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatMunicipio.Queries.GetCatMunicipio
{
    public sealed class GetCatMunicipioQueryHandler(ICatMunicipioRepository catMunicipioRepository)
        : IRequestHandler<GetCatMunicipioQuery, ResponseWrapper<IReadOnlyList<CatMunicipioDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatMunicipioDto>>> Handle(
            GetCatMunicipioQuery request,
            CancellationToken cancellationToken)
        {
            var municipios = await catMunicipioRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatMunicipioDto> response = municipios
                .Select(x => new CatMunicipioDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catálogo de municipios obtenido correctamente");
        }
    }
}
