using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Queries.GetEnlaceAcademicoById
{
    public sealed class GetEnlaceAcademicoByIdQueryHandler(
        IIdentityService identityService,
        IInstitucionRepository institucionRepository,
        ICatNivelAcademicoRepository catNivelAcademicoRepository,
        IMapUserPerfilAcademicoRepository mapUserPerfilAcademicoRepository)
        : IRequestHandler<GetEnlaceAcademicoByIdQuery, ResponseWrapper<EnlaceAcademicoDto>>
    {
        public async Task<ResponseWrapper<EnlaceAcademicoDto>> Handle(
            GetEnlaceAcademicoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var usuario = await identityService.GetUserByIdRawAsync(request.Id, cancellationToken);

            if (usuario is null)
            {
                return ResponseFactory.Failure<EnlaceAcademicoDto>("Enlace académico no encontrado", HttpStatusCode.NotFound);
            }

            var institucion = usuario.IdInstitucion.HasValue
                ? await institucionRepository.GetByIdAsync(usuario.IdInstitucion.Value, cancellationToken)
                : null;

            var niveles = await catNivelAcademicoRepository.GetAllAsync(cancellationToken);
            var nivel = niveles.FirstOrDefault(n => n.Id == usuario.IdCatNivelAcademico);

            var perfiles = await mapUserPerfilAcademicoRepository.GetByUserIdAsync(usuario.Id, cancellationToken);

            var dto = new EnlaceAcademicoDto(
                usuario.Id,
                usuario.FullName,
                usuario.Email,
                usuario.IdInstitucion ?? 0,
                institucion?.StrNombre ?? "—",
                usuario.StrRFC,
                usuario.StrSNII,
                usuario.IdCatNivelAcademico,
                nivel?.StrValor,
                usuario.StrRutaIne,
                usuario.StrRutaFotografia,
                usuario.IsActive,
                perfiles.Select(p => p.IdCatPerfilAcademico).ToList());

            return ResponseFactory.Success(dto, "Enlace académico obtenido correctamente");
        }
    }
}
