using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Users.Queries.GetEnlacesAcademicos
{
    public sealed class GetEnlacesAcademicosQueryHandler(
        IIdentityService identityService,
        IInstitucionRepository institucionRepository,
        ICatNivelAcademicoRepository catNivelAcademicoRepository,
        IMapUserPerfilAcademicoRepository mapUserPerfilAcademicoRepository)
        : IRequestHandler<GetEnlacesAcademicosQuery, ResponseWrapper<List<EnlaceAcademicoDto>>>
    {
        private const string AdminRole = "Admin";

        public async Task<ResponseWrapper<List<EnlaceAcademicoDto>>> Handle(
            GetEnlacesAcademicosQuery request,
            CancellationToken cancellationToken)
        {
            var usuarios = await identityService.GetUsersByRoleAsync(AdminRole, cancellationToken);
            var instituciones = await institucionRepository.GetAllAsync(cancellationToken);
            var niveles = await catNivelAcademicoRepository.GetAllAsync(cancellationToken);

            var dtos = new List<EnlaceAcademicoDto>();

            foreach (var usuario in usuarios)
            {
                var institucion = instituciones.FirstOrDefault(i => i.Id == usuario.IdInstitucion);
                var nivel = niveles.FirstOrDefault(n => n.Id == usuario.IdCatNivelAcademico);
                var perfiles = await mapUserPerfilAcademicoRepository.GetByUserIdAsync(usuario.Id, cancellationToken);

                dtos.Add(new EnlaceAcademicoDto(
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
                    perfiles.Select(p => p.IdCatPerfilAcademico).ToList()));
            }

            return ResponseFactory.Success(dtos, "Enlaces académicos obtenidos correctamente");
        }
    }
}
