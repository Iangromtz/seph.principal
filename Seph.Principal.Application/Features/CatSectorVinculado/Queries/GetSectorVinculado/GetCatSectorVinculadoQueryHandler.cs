using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMecanismoSeguimiento.DTOs;
using Seph.Principal.Application.Features.CatMecanismoSeguimiento.Queries.GetMecanismoSeguimiento;
using Seph.Principal.Application.Features.CatSectorVinculado.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatSectorVinculado.Queries.GetSectorVinculado
{
    public sealed class GetCatSectorVinculadoQueryHandler(ICatSectorVinculadoRepository catSectorVinculado)
        : IRequestHandler<GetCatSectorVinculadoQuery, ResponseWrapper<IReadOnlyList<CatSectorVinculadoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatSectorVinculadoDto>>> Handle(
            GetCatSectorVinculadoQuery request,
            CancellationToken cancellationToken)
        {
            var catSector = await catSectorVinculado.GetAllAsync(cancellationToken);

            IReadOnlyList<CatSectorVinculadoDto> response = catSector
                .Select(x => new CatSectorVinculadoDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Mecanismos obtenido correctamente");
        }
    }
}