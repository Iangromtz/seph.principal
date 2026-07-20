namespace Seph.Principal.Application.Features.ReporteInfraestructura.DTOs
{
    /// <summary>
    /// Representa los indicadores de infraestructura
    /// registrados durante un periodo.
    /// </summary>
    public sealed record ReporteInfraestructuraEstadisticasDto(
        string Periodo,
        int TotalAulas,
        int TotalLaboratorios,
        int TotalTalleres,
        bool CuentaConBiblioteca,
        int TotalBibliotecas,
        int TotalEquiposComputo,
        string AccesoInternet,
        string InstalacionesDiscapacidad);
}