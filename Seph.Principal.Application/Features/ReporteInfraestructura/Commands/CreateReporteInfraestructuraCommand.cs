using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Commands
{
    /// <summary>
    /// Representa la solicitud para crear un reporte
    /// de infraestructura asociado a una institución y periodo.
    /// </summary>
    public sealed record CreateReporteInfraestructuraCommand(
        long IdMapInstitucionPeriodo,
        int IntTotalAulas,
        int IntTotalLaboratorios,
        int IntTotalTalleres,
        bool BitBiblioteca,
        int IntTotalBibliotecas,
        int IntTotalComputo,
        long IdInternet,
        long IdDiscapacitado,
        Guid IdUsuarioRegistro)
        : IRequest<ResponseWrapper<ReporteInfraestructuraDto>>
    {
    }
}