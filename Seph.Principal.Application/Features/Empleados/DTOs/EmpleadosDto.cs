namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    public sealed record EmpleadosDto(
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp,
        long IdSexo,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo,
        DateTime DateTimeFechaBaja
        );
}
