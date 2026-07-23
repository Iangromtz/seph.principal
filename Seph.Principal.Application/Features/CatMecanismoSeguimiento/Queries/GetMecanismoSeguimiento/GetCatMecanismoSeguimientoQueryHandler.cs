using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatMecanismoSeguimiento.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatMecanismoSeguimiento.Queries.GetMecanismoSeguimiento
{
    public sealed class GetCatMecanismoSeguimientoQueryHandler(ICatMecanismoSeguimientoRepository catMecanismoSeguimiento)
        : IRequestHandler<GetCatMecanismoSeguimientoQuery, ResponseWrapper<IReadOnlyList<CatMecanismoSeguimientoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatMecanismoSeguimientoDto>>> Handle(
            GetCatMecanismoSeguimientoQuery request,
            CancellationToken cancellationToken)
        {
            var catMecanismo = await catMecanismoSeguimiento.GetAllAsync(cancellationToken);

            IReadOnlyList<CatMecanismoSeguimientoDto> response = catMecanismo
                .Select(x => new CatMecanismoSeguimientoDto(
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