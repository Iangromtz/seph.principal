using System;

namespace Seph.Principal.Application.Features.ReporteInfraestructura.DTOs
{
    /// <summary>
    /// Representa la información de infraestructura
    /// que se devuelve desde la capa Application.
    /// </summary>
    public sealed record ReporteInfraestructuraDto(
        long Id,
        long IdMapInstitucionPeriodo,
        int IntTotalAulas,
        int IntTotalLaboratorios,
        int IntTotalTalleres,
        bool BitBiblioteca,
        int IntTotalBibliotecas,
        int IntTotalComputo,
        long IdInternet,
        long IdDiscapacitado,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo);
}