using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatArea.DTOs;
using Seph.Principal.Application.Features.CatArea.Queries.GetArea;
using Seph.Principal.Application.Features.CatInternet.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatInternet.Queries.GetInternet
{
    public sealed class GetCatInternetQueryHandler(ICatInternetRepository catInternetRepository)
        : IRequestHandler<GetCatInternetQuery, ResponseWrapper<IReadOnlyList<CatInternetDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatInternetDto>>> Handle(
            GetCatInternetQuery request,
            CancellationToken cancellationToken)
        {
            var catInternet = await catInternetRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatInternetDto> response = catInternet
                .Select(x => new CatInternetDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de internet obtenido correctamente");
        }
    }
}