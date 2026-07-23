using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatTipoPeriodo.Queries
{
    /// <summary>
    /// Obtiene el catálogo de tipos de periodo.
    /// </summary>
    public sealed class GetCatTipoPeriodoQueryHandler(
        ICatTipoPeriodoRepository catTipoPeriodoRepository)
        : IRequestHandler<
            GetCatTipoPeriodoQuery,
            ResponseWrapper<
                IReadOnlyList<CatTipoPeriodoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyList<CatTipoPeriodoDto>>> Handle(
            GetCatTipoPeriodoQuery request,
            CancellationToken cancellationToken)
        {
            var tiposPeriodo =
                await catTipoPeriodoRepository.GetAllAsync(
                    cancellationToken);

            IReadOnlyList<CatTipoPeriodoDto> response =
                tiposPeriodo
                    .Select(x => new CatTipoPeriodoDto(
                        x.Id,
                        x.StrValor,
                        x.StrDescripcion,
                        x.IntNumeroMeses,
                        x.BitActivo))
                    .ToList();

            return ResponseFactory.Success(
                response,
                "Catálogo de tipos de periodo obtenido correctamente");
        }
    }
}
