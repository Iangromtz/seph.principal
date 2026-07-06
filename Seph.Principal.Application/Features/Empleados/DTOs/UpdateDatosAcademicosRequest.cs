using System.Collections.Generic;

namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    public record UpdateDatosAcademicosRequest(
        string? StrSNII,
        IReadOnlyList<long> IdsPerfilAcademico
    );
}
