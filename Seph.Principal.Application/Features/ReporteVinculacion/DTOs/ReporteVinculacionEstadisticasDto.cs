namespace Seph.Principal.Application.Features.ReporteVinculacion.DTOs
{
    /// <summary>
    /// Representa las estadísticas generales
    /// del reporte de vinculación.
    /// </summary>
    public sealed record ReporteVinculacionEstadisticasDto(
        /// <summary>
        /// Nombre del periodo seleccionado.
        /// </summary>
        string Periodo,

        /// <summary>
        /// Total de convenios activos registrados.
        /// </summary>
        int TotalConveniosActivos);
}