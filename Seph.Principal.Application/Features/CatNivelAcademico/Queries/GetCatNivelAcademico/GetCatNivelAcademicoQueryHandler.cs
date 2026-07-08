using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatNivelAcademico.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatNivelAcademico.Queries.GetCatNivelAcademico
{
    public sealed class GetCatNivelAcademicoQueryHandler(ICatNivelAcademicoRepository repository)
         : IRequestHandler<GetCatNivelAcademicoQuery, ResponseWrapper<List<CatNivelAcademicoDto>>>
    {
        public async Task<ResponseWrapper<List<CatNivelAcademicoDto>>> Handle(GetCatNivelAcademicoQuery request, CancellationToken cancellationToken)
        {
            var niveles = await repository.GetAllAsync(cancellationToken);
            var dtos = niveles.Select(x => new CatNivelAcademicoDto(x.Id, x.StrValor, x.StrDescripcion)).ToList();
            return ResponseFactory.Success(dtos, "Niveles académicos obtenidos correctamente");
        }
    }
}
