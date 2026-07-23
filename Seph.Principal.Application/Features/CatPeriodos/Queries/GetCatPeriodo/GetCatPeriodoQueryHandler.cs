using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPeriodos.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPeriodos.Queries.GetCatPeriodo
{
    public sealed class GetCatPeriodoQueryHandler(
         ICatPeriodoRepository catPeriodoRepository)
         : IRequestHandler<
             GetCatPeriodoQuery,
             ResponseWrapper<IReadOnlyList<CatPeriodoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatPeriodoDto>>> Handle(
            GetCatPeriodoQuery request,
            CancellationToken cancellationToken)
        {
            // Consulta los periodos registrados en el catálogo.
            var catPeriodo = await catPeriodoRepository.GetAllAsync(
                cancellationToken);

            // Convierte las entidades del dominio a DTO.
            IReadOnlyList<CatPeriodoDto> response = catPeriodo
                .Select(x => new CatPeriodoDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion,
                    x.IntAnio,
                    x.IntNumeroPeriodo,
                    x.DateFechaInicio,
                    x.DateFechaFin,
                    x.BitActivo,
                    x.IdTipoPeriodo,
                    x.TipoPeriodo.StrValor))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catálogo de periodos obtenido correctamente");
        }
    }
}
