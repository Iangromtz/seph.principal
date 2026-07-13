using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo
{
    public sealed class CreateMapInstitucionPeriodoCommandHandler(
          IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
          IUnitOfWork unitOfWork)
          : IRequestHandler<CreateMapInstitucionPeriodoCommand, ResponseWrapper<MapInstitucionPeriodoDto>>
    {
        public async Task<ResponseWrapper<MapInstitucionPeriodoDto>> Handle(
            CreateMapInstitucionPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Evita crear dos veces el mismo periodo para la misma institución.
            var exists = await mapInstitucionPeriodoRepository.GetByInstitucionPeriodoAsync(
                request.IdInstitucion,
                request.IdPeriodo,
                cancellationToken);

            if (exists is not null)
            {
                return ResponseFactory.Failure<MapInstitucionPeriodoDto>(
                    "La institución ya tiene registrado este periodo.",
                    HttpStatusCode.BadRequest);
            }

            var mapInstitucionPeriodo = new Domain.Entities.MapInstitucionPeriodo
            {
                IdInstitucion = request.IdInstitucion,
                IdPeriodo = request.IdPeriodo,
                BitCapturaAbierta = request.BitCapturaAbierta,
                DateFechaApertura = request.DateFechaApertura,
                DateFechaCierre = request.DateFechaCierre,
                DateTimeFechaRegistro = DateTime.Now,
                IdUsuarioRegistro = request.IdUsuarioRegistro,
                BitActivo = true
            };

            await mapInstitucionPeriodoRepository.AddAsync(
                mapInstitucionPeriodo,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new MapInstitucionPeriodoDto(
                mapInstitucionPeriodo.Id,
                mapInstitucionPeriodo.IdInstitucion,
                mapInstitucionPeriodo.IdPeriodo,
                mapInstitucionPeriodo.BitCapturaAbierta,
                mapInstitucionPeriodo.DateFechaApertura,
                mapInstitucionPeriodo.DateFechaCierre,
                mapInstitucionPeriodo.DateTimeFechaRegistro,
                mapInstitucionPeriodo.IdUsuarioRegistro,
                mapInstitucionPeriodo.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Periodo asignado a la institución correctamente");
        }
    }
}
