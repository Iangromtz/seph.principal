using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.UpdateEnlace
{
    public sealed class UpdateEnlaceCommandHandler(
        IIdentityService identityService,
        IMapUserPerfilAcademicoRepository mapUserPerfilAcademicoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateEnlaceCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(UpdateEnlaceCommand request, CancellationToken cancellationToken)
        {
            var ok = await identityService.UpdateUserDetailsAsync(
                request.Id, request.FullName, request.IdInstitucion,
                request.StrRutaIne, request.StrRutaFotografia, request.StrRFC, request.StrSNII, request.IdNivelAcademico,
                cancellationToken);

            if (!ok)
            {
                return ResponseFactory.Failure<string>("Enlace académico no encontrado", HttpStatusCode.NotFound);
            }

            await mapUserPerfilAcademicoRepository.DeleteByUserIdAsync(request.Id, cancellationToken);

            foreach (var idPerfilAcademico in request.IdsPerfilAcademico.Distinct())
            {
                await mapUserPerfilAcademicoRepository.AddAsync(
                    new MapUserPerfilAcademico(request.Id, idPerfilAcademico), cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success(
                "Enlace académico actualizado correctamente",
                "Enlace académico actualizado correctamente");
        }
    }
}
