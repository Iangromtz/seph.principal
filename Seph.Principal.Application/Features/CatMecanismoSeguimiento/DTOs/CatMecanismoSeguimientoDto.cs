using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatMecanismoSeguimiento.DTOs
{
    public sealed record CatMecanismoSeguimientoDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
