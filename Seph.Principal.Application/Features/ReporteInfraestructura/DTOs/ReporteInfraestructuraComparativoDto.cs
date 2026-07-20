namespace Seph.Principal.Application.Features.ReporteInfraestructura.DTOs
{
    /// <summary>
    /// Representa la comparación de un indicador
    /// de infraestructura entre dos periodos.
    /// </summary>
    public sealed record ReporteInfraestructuraComparativoDto(
        string Indicador,
        string PeriodoActual,
        int ValorActual,
        string? PeriodoAnterior,
        int? ValorAnterior,
        int Diferencia,
        decimal Porcentaje,
        string Estado);
}