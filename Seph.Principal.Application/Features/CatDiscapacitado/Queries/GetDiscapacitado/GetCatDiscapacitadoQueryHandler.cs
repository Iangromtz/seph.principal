using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatArea.DTOs;
using Seph.Principal.Application.Features.CatArea.Queries.GetArea;
using Seph.Principal.Application.Features.CatDiscapacitado.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatDiscapacitado.Queries.GetDiscapacitado
{
    public sealed class GetCatDiscapacitadoQueryHandler(ICatDiscapacitadoRepository catDiscapacitadoRepository)
        : IRequestHandler<GetCatDiscapacitadoQuery, ResponseWrapper<IReadOnlyList<CatDiscapacitadoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatDiscapacitadoDto>>> Handle(
            GetCatDiscapacitadoQuery request,
            CancellationToken cancellationToken)
        {
            var catDiscapacitado = await catDiscapacitadoRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatDiscapacitadoDto> response = catDiscapacitado
                .Select(x => new CatDiscapacitadoDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Discapacitado obtenido correctamente");
        }
    }
}