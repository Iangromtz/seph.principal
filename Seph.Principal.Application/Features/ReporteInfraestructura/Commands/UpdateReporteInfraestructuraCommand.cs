using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.Commands
{
    /*
     * Actualiza el reporte de infraestructura asociado
     * a una relación institución-periodo.
     */
    public sealed record UpdateReporteInfraestructuraCommand(
        long IdMapInstitucionPeriodo,
        int IntTotalAulas,
        int IntTotalLaboratorios,
        int IntTotalTalleres,
        bool BitBiblioteca,
        int IntTotalBibliotecas,
        int IntTotalComputo,
        long IdInternet,
        long IdDiscapacitado)
        : IRequest<ResponseWrapper<ReporteInfraestructuraDto>>;
}