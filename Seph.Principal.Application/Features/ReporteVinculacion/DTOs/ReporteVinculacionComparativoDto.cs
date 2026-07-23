namespace Seph.Principal.Application.Features.ReporteVinculacion.DTOs
{
    /// <summary>
    /// Representa el comparativo de un indicador
    /// del reporte de vinculación entre el periodo
    /// actual y el periodo anterior.
    /// </summary>
    public sealed record ReporteVinculacionComparativoDto(
        /// <summary>
        /// Nombre del indicador comparado.
        /// </summary>
        string Indicador,

        /// <summary>
        /// Nombre del periodo actual.
        /// </summary>
        string PeriodoActual,

        /// <summary>
        /// Valor del indicador en el periodo actual.
        /// </summary>
        int ValorActual,

        /// <summary>
        /// Nombre del periodo anterior.
        /// </summary>
        string? PeriodoAnterior,

        /// <summary>
        /// Valor del indicador en el periodo anterior.
        /// </summary>
        int? ValorAnterior,

        /// <summary>
        /// Diferencia entre ambos periodos.
        /// </summary>
        int Diferencia,

        /// <summary>
        /// Porcentaje de cambio respecto al periodo anterior.
        /// </summary>
        decimal PorcentajeCambio,

        /// <summary>
        /// Estado del indicador.
        /// </summary>
        string Estado);
}