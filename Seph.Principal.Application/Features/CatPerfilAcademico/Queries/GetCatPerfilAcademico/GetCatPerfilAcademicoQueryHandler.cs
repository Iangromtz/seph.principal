using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPerfilAcademico.DTOs;
using Seph.Principal.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPerfilAcademico.Queries.GetCatPerfilAcademico
{
    public sealed class GetCatPerfilAcademicoQueryHandler(ICatPerfilAcademicoRepository catPerfilAcademicoRepository)
        : IRequestHandler<GetCatPerfilAcademicoQuery, ResponseWrapper<IReadOnlyList<CatPerfilAcademicoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatPerfilAcademicoDto>>> Handle(
            GetCatPerfilAcademicoQuery request,
            CancellationToken cancellationToken)
        {
            var perfiles = await catPerfilAcademicoRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatPerfilAcademicoDto> response = perfiles
                .Select(x => new CatPerfilAcademicoDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Perfiles Academicos obtenido correctamente");
        }
    }
}
