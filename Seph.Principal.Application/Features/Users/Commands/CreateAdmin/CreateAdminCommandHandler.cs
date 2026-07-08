using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.CreateAdmin
{
    public sealed class CreateAdminCommandHandler(
    IIdentityService identityService,
    IMapUserPerfilAcademicoRepository mapUserPerfilAcademicoRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAdminCommand, ResponseWrapper<UserCreatedDto>>
    {
        private const string AdminRole = "Admin";

        public async Task<ResponseWrapper<UserCreatedDto>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.CreateUserWithRoleAsync(
                request.FullName, request.Email, request.Password, AdminRole, request.IdInstitucion,
                request.StrRutaIne, request.StrRutaFotografia, request.StrRFC, request.StrSNII, request.IdNivelAcademico,
                cancellationToken);

            if (userId is null)
                return ResponseFactory.Failure<UserCreatedDto>("El correo ya está registrado", HttpStatusCode.Conflict);

            foreach (var idPerfilAcademico in request.IdsPerfilAcademico.Distinct())
            {
                await mapUserPerfilAcademicoRepository.AddAsync(
                    new MapUserPerfilAcademico(userId.Value, idPerfilAcademico), cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new UserCreatedDto(userId.Value, request.Email, request.FullName, AdminRole, request.IdInstitucion);
            return ResponseFactory.Success(dto, "Enlace académico registrado correctamente");
        }
    }
}
