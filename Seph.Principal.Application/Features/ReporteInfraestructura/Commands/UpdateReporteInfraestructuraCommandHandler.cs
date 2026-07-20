using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Commands
{
    /// <summary>
    /// Procesa la actualización de un reporte de infraestructura.
    /// </summary>
    public sealed class UpdateReporteInfraestructuraCommandHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateReporteInfraestructuraCommand,
            ResponseWrapper<ReporteInfraestructuraDto>>
    {
        /// <summary>
        /// Actualiza la información capturable de un reporte de infraestructura.
        /// </summary>
        public async Task<ResponseWrapper<ReporteInfraestructuraDto>> Handle(
            UpdateReporteInfraestructuraCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte asociado a la institución y periodo.
            var reporte = await reporteInfraestructuraRepository
                .GetByMapInstitucionPeriodoForUpdateAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteInfraestructuraDto>(
                    "No existe un reporte de infraestructura para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Actualiza únicamente la información capturable.
            reporte.IntTotalAulas =
                request.IntTotalAulas;

            reporte.IntTotalLaboratorios =
                request.IntTotalLaboratorios;

            reporte.IntTotalTalleres =
                request.IntTotalTalleres;

            reporte.BitBiblioteca =
                request.BitBiblioteca;

            reporte.IntTotalBibliotecas =
                request.IntTotalBibliotecas;

            reporte.IntTotalComputo =
                request.IntTotalComputo;

            reporte.IdInternet =
                request.IdInternet;

            reporte.IdDiscapacitado =
                request.IdDiscapacitado;

            await unitOfWork.SaveChangesAsync(
                cancellationToken);


            await bitacoraService.RegistrarAsync(
                "Infraestructura",
                reporte.Id.ToString(),
                "Editar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporte,
                cancellationToken);

            var dto = new ReporteInfraestructuraDto(
                reporte.Id,
                reporte.IdMapInstitucionPeriodo,
                reporte.IntTotalAulas,
                reporte.IntTotalLaboratorios,
                reporte.IntTotalTalleres,
                reporte.BitBiblioteca,
                reporte.IntTotalBibliotecas,
                reporte.IntTotalComputo,
                reporte.IdInternet,
                reporte.IdDiscapacitado,
                reporte.DateTimeFechaRegistro,
                reporte.IdUsuarioRegistro,
                reporte.BitActivo);

            return ResponseFactory.Success(
                dto,
                "Reporte de infraestructura actualizado correctamente");
        }
    }
}