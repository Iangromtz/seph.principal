using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Commands
{
    /// <summary>
    /// Procesa la creación de un reporte de infraestructura.
    /// </summary>
    public sealed class CreateReporteInfraestructuraCommandHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateReporteInfraestructuraCommand,
            ResponseWrapper<ReporteInfraestructuraDto>>
    {
        /// <summary>
        /// Crea un reporte de infraestructura para una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteInfraestructuraDto>> Handle(
            CreateReporteInfraestructuraCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo institucional.
            var exists =
                await reporteInfraestructuraRepository
                    .ExistsByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<ReporteInfraestructuraDto>(
                    "Ya existe un reporte de infraestructura registrado para este periodo.",
                    HttpStatusCode.BadRequest);
            }

            var reporteInfraestructura =
                new Domain.Entities.ReporteInfraestructura
                {
                    IdMapInstitucionPeriodo =
                        request.IdMapInstitucionPeriodo,

                    IntTotalAulas =
                        request.IntTotalAulas,

                    IntTotalLaboratorios =
                        request.IntTotalLaboratorios,

                    IntTotalTalleres =
                        request.IntTotalTalleres,

                    BitBiblioteca =
                        request.BitBiblioteca,

                    IntTotalBibliotecas =
                        request.IntTotalBibliotecas,

                    IntTotalComputo =
                        request.IntTotalComputo,

                    IdInternet =
                        request.IdInternet,

                    IdDiscapacitado =
                        request.IdDiscapacitado,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo =
                        true
                };

            await reporteInfraestructuraRepository.AddAsync(
                reporteInfraestructura,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);


            await bitacoraService.RegistrarAsync(
                "Infraestructura",
                reporteInfraestructura.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporteInfraestructura,
                cancellationToken);

            var dto =
                new ReporteInfraestructuraDto(
                    reporteInfraestructura.Id,
                    reporteInfraestructura.IdMapInstitucionPeriodo,
                    reporteInfraestructura.IntTotalAulas,
                    reporteInfraestructura.IntTotalLaboratorios,
                    reporteInfraestructura.IntTotalTalleres,
                    reporteInfraestructura.BitBiblioteca,
                    reporteInfraestructura.IntTotalBibliotecas,
                    reporteInfraestructura.IntTotalComputo,
                    reporteInfraestructura.IdInternet,
                    reporteInfraestructura.IdDiscapacitado,
                    reporteInfraestructura.DateTimeFechaRegistro,
                    reporteInfraestructura.IdUsuarioRegistro,
                    reporteInfraestructura.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de infraestructura registrado correctamente");
        }
    }
}